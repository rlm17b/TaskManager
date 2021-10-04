using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ManageTasks
{
    public class ListNavigator<T>
    {
        private int pageSize;
        private int currentPg;
        private List<T> state;
        private int lastPg
        {
            get
            {
                var val = state.Count / pageSize;

                if (state.Count / pageSize > 0)
                {
                    val++;
                }

                return val;
            }
        }

        public bool hasPreviousPg
        {
            get
            {
                return currentPg > 1;
            }
        }

        public bool hasNextPg
        {
            get
            {
                return currentPg < lastPg;
            }
        }

        public ListNavigator(List<T> list, int pageSize = 5)
        {
            this.pageSize = pageSize;
            this.currentPg = 1;
            state = list;
        }

        public Dictionary<int, T> GoForward()
        {
            if(currentPg + 1 > lastPg)
            {
                throw new PageFaultException("Cannot Navigate to the next page");
            }
            currentPg++;
            return GetWindow();
        }

        public Dictionary<int, T> GoBackward()
        {
            if (currentPg - 1 <= 0)
            {
                throw new PageFaultException("Cannot Navigate to the previous page");
            }
            currentPg--;
            return GetWindow();
        }

        public Dictionary<int, T> GoToPg(int pg)
        {
            if (pg <= 0 || pg > lastPg)
            {
                throw new PageFaultException("Cannot Navigate to the page that is out of bounds");
            }
            currentPg = pg;
            return GetWindow();
        }

        public Dictionary<int, T> GetCurrentPg()
        {
            return GoToPg(currentPg);
        }

        public Dictionary<int, T> GoToFirstPg()
        {
            currentPg = 1;
            return GetWindow();
        }

        public Dictionary<int, T> GoToLastPg()
        {
            currentPg = lastPg;
            return GetWindow();
        }


        private Dictionary<int, T> GetWindow()
        {
            var window = new Dictionary<int, T>();
            for(int i = (currentPg - 1) * pageSize; i < (currentPg - 1) * pageSize + pageSize && i < state.Count; i++)
            {
                window.Add(i + 1, state[i]);
            }

            return window;
        }

        public class PageFaultException : Exception
        {
            public PageFaultException(string message) : base(message) {
            }
        }
    }
}
