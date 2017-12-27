using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace DroidTestApp
{
    [Activity(Label = "DroidTestApp", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private MainAdapter _adapter;
        private bool _hideEmpty;
        private bool _showFooters = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            RecyclerView list = (RecyclerView)FindViewById(Resource.Id.list);
            _adapter = new MainAdapter();
            GridLayoutManager manager =
                new GridLayoutManager(this, Resources.GetInteger(Resource.Integer.grid_span));
            list.SetLayoutManager(manager);
            _adapter.SetLayoutManager(manager);
            _adapter.ShouldShowHeadersForEmptySections(_hideEmpty);
            _adapter.ShouldShowFooters(_showFooters);
            list.SetAdapter(_adapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            menu.FindItem(Resource.Id.hide_empty_sections).SetChecked(!_hideEmpty);
            menu.FindItem(Resource.Id.show_footers).SetChecked(_showFooters);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.hide_empty_sections)
            {
                _hideEmpty = !_hideEmpty;
                _adapter.ShouldShowHeadersForEmptySections(_hideEmpty);
                item.SetChecked(!_hideEmpty);
                return true;
            }
            else if (item.ItemId == Resource.Id.show_footers)
            {
                _showFooters = !_showFooters;
                _adapter.ShouldShowFooters(_showFooters);
                item.SetChecked(_showFooters);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
