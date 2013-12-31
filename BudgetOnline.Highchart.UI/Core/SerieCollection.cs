//
//  Copyright (c) 2010, 100loop
//  All rights reserved.
//
//  Authors: 
//           
//           * André Paulovich (paulovich@100loop.com)
//           Blog: http://www.100loop.com/          
//           Talk: andre.paulovich@gmail.com 
//

using System.Collections.Generic;
using System.Web.UI;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core
{

    public class SerieCollection : List<Serie>, IStateManager
    {

        private bool _marked;
        public SerieCollection()
        {
            _marked = false;
        }

        public bool IsTrackingViewState
        {
            get { return _marked; }
        }

        public void LoadViewState(object state)
        {
            if (state != null)
            {

                var t = (Serie[])state;

                Clear();

                foreach (Serie item in t)
                {
                    Add(item);
                }

            }
        }

        public object SaveViewState()
        {

            return ToArray();

        }

        public void TrackViewState()
        {
            _marked = true;
        }

        public override string ToString()
        {
            
            var keys = new List<string>();

            foreach (Serie serie in this)
            {

                string ignored = JsonConvert.SerializeObject(serie, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                keys.Add(ignored);

            }

            return string.Format("series: [{0}]", string.Join(",", keys.ToArray()));            

        }

    }
}