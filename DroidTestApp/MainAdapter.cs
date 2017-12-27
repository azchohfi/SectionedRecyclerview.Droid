using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SectionedRecyclerview.Droid;

namespace DroidTestApp
{
    public class MainAdapter : SectionedRecyclerViewAdapter
    {
        public override int SectionCount => 20;

        public override int GetItemCount(int section)
        {
            switch (section)
            {
                case 0:
                    return 4;
                case 1:
                    return 0;
                case 2:
                    return 10;
                default:
                    return 6;
            }
        }

        public override void OnBindHeaderViewHolder(Object holder, int section, bool expanded)
        {
            if (holder is MainVh mainVhHolder)
            {
                mainVhHolder.Title.Text = $"Section Header {section}";
                mainVhHolder.Caret.SetImageResource(expanded
                    ? Resource.Drawable.ic_collapse
                    : Resource.Drawable.ic_expand);
            }
        }

        public override void OnBindViewHolder(Object holder, int section, int relativePosition, int absolutePosition)
        {
            // Setup non-header view.
            // 'section' is section index.
            // 'relativePosition' is index in this section.
            // 'absolutePosition' is index out of all non-header items.
            // See sample project for a visual of how these indices work.
            if (holder is MainVh mainVhHolder)
            {
                mainVhHolder.Title.Text = $"S:{section}, P:{relativePosition}, A:{absolutePosition}";
            }
        }

        public override void OnBindFooterViewHolder(Object holder, int section)
        {
            if (holder is MainVh mainVhHolder)
            {
                mainVhHolder.Title.Text = $"Section footer {section}";
            }
        }

        public override int GetItemViewType(int section, int relativePosition, int absolutePosition)
        {
            if (section == 1)
            {
                // VIEW_TYPE_FOOTER is -3, VIEW_TYPE_HEADER is -2, VIEW_TYPE_ITEM is -1.
                // You can return 0 or greater.
                return 0;
            }
            return base.GetItemViewType(section, relativePosition, absolutePosition);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layout;
            switch ((SectionedRecyclerViewAdapterViewType)viewType)
            {
                case SectionedRecyclerViewAdapterViewType.Header:
                    layout = Resource.Layout.list_item_header;
                    break;
                case SectionedRecyclerViewAdapterViewType.Item:
                    layout = Resource.Layout.list_item_main;
                    break;
                case SectionedRecyclerViewAdapterViewType.Footer:
                    layout = Resource.Layout.list_item_footer;
                    break;
                default:
                    // Our custom item, which is the 0 returned in getItemViewType() above
                    layout = Resource.Layout.list_item_main_bold;
                    break;
            }

            View v = LayoutInflater.From(parent.Context).Inflate(layout, parent, false);
            return new MainVh(v, this);
        }

        private class MainVh : SectionedViewHolder, View.IOnClickListener
        {
            public readonly TextView Title;
            public readonly ImageView Caret;
            private readonly MainAdapter _adapter;
            private Toast _toast;

            public MainVh(View itemView, MainAdapter adapter) :
                base(itemView)
            {
                Title = itemView.FindViewById<TextView>(Resource.Id.title);
                Caret = itemView.FindViewById<ImageView>(Resource.Id.caret);
                _adapter = adapter;
                itemView.SetOnClickListener(this);
            }


            public void OnClick(View view)
            {
                if (IsFooter)
                {
                    // ignore footer clicks
                    return;
                }

                if (IsHeader)
                {
                    _adapter.ToggleSectionExpanded(RelativePosition.Section());
                }
                else
                {
                    _toast?.Cancel();
                    _toast = Toast.MakeText(view.Context, RelativePosition.ToString(), ToastLength.Short);
                    _toast.Show();
                }
            }
        }
    }
}