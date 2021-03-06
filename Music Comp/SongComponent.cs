﻿using System.Collections.Generic;
using System.Drawing;
using System;

namespace Music_Comp
{
    [Serializable]
    public class SongComponent
    {
        protected bool isSelected;
        protected RectangleF area;
        public RectangleF GetArea()
        {
            return area;
        }

        public void Select()
        {
            isSelected = true;
        }

        public bool isItSelected()
        {
            return isSelected;
        }

        public void Deselect()
        {
            isSelected = false;
        }
    }
}
