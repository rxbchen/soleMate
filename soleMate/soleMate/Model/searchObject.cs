using System;
using System.Collections.Generic;
using System.Text;

namespace soleMate.Model
{
    public class SearchObject
    {
        public String Model { get; set; }

        public float Size { get; set; }

        public SearchObject(String model, float size)
        {
            this.Size = size;
            this.Model = model;
        }

    }
}
