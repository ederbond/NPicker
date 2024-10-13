namespace NPicker.Samples
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    _picker.Date = null;
        //}

        //private void OnShowCurrentValue(object sender, EventArgs e)
        //{
        //    LabelCurrentValue.Text = _picker.Date == null ? "Null" : _picker.Date.ToString();
        //}
    }
}
