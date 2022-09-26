using System.Collections;
using System.Collections.Generic;

namespace Global.Shared.Commons
{
    public class SortingConditionQueue<TEntity> : IEnumerable<SortingCondition<TEntity>>
    {
        private readonly LinkedList<SortingCondition<TEntity>> _underlyingSortingConditionQueue;

        public SortingConditionQueue()
        {
            _underlyingSortingConditionQueue = new LinkedList<SortingCondition<TEntity>>();
        }

        public SortingConditionQueue<TEntity> Add(
            SortingCondition<TEntity> sortingCondition)
        {
            _underlyingSortingConditionQueue.AddLast(sortingCondition);

            // return this object to enable function chaining
            return this;
        }

        public SortingConditionQueue<TEntity> Remove()
        {
            _underlyingSortingConditionQueue.RemoveLast();

            // return this object to enable function chaining
            return this;
        }

        public IEnumerator<SortingCondition<TEntity>> GetEnumerator()
        {
            return _underlyingSortingConditionQueue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
