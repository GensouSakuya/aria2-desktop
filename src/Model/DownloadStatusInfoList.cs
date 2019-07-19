using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class DownloadStatusInfoList
    {
        private List<DownloadStatusInfo> _list = new List<DownloadStatusInfo>();
        private volatile object locker = new object();

        public List<DownloadStatusInfo> List
        {
            get
            {
                var tempList = new List<DownloadStatusInfo>();
                lock (locker)
                {
                    _list.ForEach(item => tempList.Add(item));
                }
                return tempList;
            }
            set
            {
                lock (locker)
                {
                    _list = value;
                }
            }
        }

        public List<DownloadStatusInfo> GetWithCondition(Func<DownloadStatusInfo, bool> condition)
        {
            var tempList = new List<DownloadStatusInfo>();
            lock (locker)
            {
                tempList = _list.Where(condition).ToList();
            }
            return tempList;
        }

        public List<DownloadStatusInfo> RemoveAll(Predicate<DownloadStatusInfo> condition)
        {
            var tempList = new List<DownloadStatusInfo>();
            lock (locker)
            {
                _list.RemoveAll(condition);
                _list.ForEach(item => tempList.Add(item));
            }
            return tempList;
        }

        public List<DownloadStatusInfo> AddRange(IEnumerable<DownloadStatusInfo> list)
        {
            var tempList = new List<DownloadStatusInfo>();
            lock (locker)
            {
                _list.AddRange(list);
                _list.ForEach(item => tempList.Add(item));
            }
            return tempList;
        }

        public DownloadStatusInfoList() { }

        public DownloadStatusInfoList(IEnumerable<DownloadStatusInfo> list)
        {
            _list = list.ToList();
        }
    }
}
