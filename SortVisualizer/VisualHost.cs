using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SortVisualizer
{
    public class VisualHost : UIElement
    {
        private VisualCollection _children;

        public VisualHost()
        {
            _children = new VisualCollection(this);

        }
        public VisualHost(Visual visual)
        {
            _children = new VisualCollection(this);
            _children.Add(visual);
        }
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        public void ChangeVisual(Visual visual)
        {
            _children.Clear();
            _children.Add(visual);
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }
    }
}
