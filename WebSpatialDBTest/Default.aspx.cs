using System;
using System.Web;
using System.Web.UI;

namespace WebSpatialDBTest
{

    public partial class Default : System.Web.UI.Page
    {
        public void button1Clicked(object sender, EventArgs args)
        {
            SpatialDBTest.Test();

            button1.Text = "You clicked me";
        }
    }
}
