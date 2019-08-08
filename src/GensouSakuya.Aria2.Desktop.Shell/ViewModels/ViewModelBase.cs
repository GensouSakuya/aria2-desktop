using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public Window Self { get; set; }

        public static void ViewModelListMerge<T>(IList<T> origin, IList<T> target) where T : IDataMergable
        {
            var originKeys = origin.Select(p => p.GetKey());
            var targetKeys = target.Select(p => p.GetKey());
            var allItemKeys = originKeys.Union(targetKeys).ToList();


            foreach (var key in allItemKeys)
            {
                var targetItem = target.FirstOrDefault(p => p.GetKey().Equals(key));
                var originItem = origin.FirstOrDefault(p => p.GetKey().Equals(key));

                //增
                if (originItem == null)
                {
                    origin.Add(targetItem);
                    return;
                }

                //删
                if (targetItem == null)
                {
                    origin.Remove(originItem);
                    return;
                }

                //改
                if (!targetItem.Equals(originItem))
                {
                    originItem.Update(targetItem);
                    return;
                }
            }
        }
    }

    public interface IDataMergable
    {
        object GetKey();
        void Update(IDataMergable data);
    }
}
