using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Venue : System.Web.UI.Page
{

    GetterService.ServiceClient firstServices = new GetterService.ServiceClient();
    VenueRegLog.VenueRegistrationLoginServiceClient secondServices = new VenueRegLog.VenueRegistrationLoginServiceClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        var userKey = Session["Userkey"];
        if (userKey == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            Fill_Artist_Dropdown();
            Fill_Show_Dropdown();
        }
    }

    protected void ArtistAdd()
    {
        //VenueRegLog.VenueRegistrationLoginServiceClient newArtist = new VenueRegLog.VenueRegistrationLoginServiceClient();
        VenueRegLog.Artist a = new VenueRegLog.Artist();
        a.ArtistName = ArtistNameTextBox.Text;
        a.ArtistEmail = ArtistEmailTextBox.Text;
        a.ArtistWebPage = ArtistWebsiteTextBox.Text;

        bool result = secondServices.AddArtist(a);
        if (result)
        {
            ArtistErrorLabel.Text = "Artist Successfully Added";
        }
        else
        {
            ArtistErrorLabel.Text = "Artist already exists";
        }

    }
    protected void artistSubmit_Click(object sender, EventArgs e)
    {

        ArtistAdd();
        Fill_Artist_Dropdown();
    }
    protected void ShowAdd()
    {
        //VenueRegLog.VenueRegistrationLoginServiceClient newShow = new VenueRegLog.VenueRegistrationLoginServiceClient();
        VenueRegLog.Show s = new VenueRegLog.Show();
        s.ShowName = ShowNameTextBox.Text;
        s.ShowDate = Convert.ToDateTime(ShowDateTextBox.Text);
        s.ShowTime = TimeSpan.Parse(ShowTimeTextBox.Text);
        s.ShowTicketInfo = TicketInfoTextBox.Text;
        s.VenueKey = Convert.ToInt32(Session["Userkey"]);

        bool result = secondServices.AddShow(s);
        if (result)
        {
            ShowErrorLabel.Text = "Show Added";
        }
        else
        {
            ShowErrorLabel.Text = "Show already exists";
        }
    }

    protected void showSubmit_Click(object sender, EventArgs e)
    {
        ShowAdd();
        Fill_Show_Dropdown();
    }

    protected void DetailAdd()
    {
        
        VenueRegLog.ShowDetail sd = new VenueRegLog.ShowDetail();
        sd.ShowKey = Convert.ToInt32(ShowsDropDownList.SelectedValue);
        sd.ArtistKey = Convert.ToInt32(ArtistDropDownList.SelectedValue);
        sd.ShowDetailArtistStartTime = TimeSpan.Parse(StartTimeTextBox.Text);
        sd.ShowDetailAdditional = AdditionalTextBox.Text;

        bool result = secondServices.AddShowDetail(sd);

        if(result)
        {
            DetailsError.Text = "Show Details Added";
        }
        else
        {
            DetailsError.Text = "Error!!";
        }

    }
    protected void showDetailsButton_Click(object sender, EventArgs e)
    {
        DetailAdd();
    }

    protected void Fill_Artist_Dropdown()
    {
        GetterService.ArtistNames[] artists = firstServices.GetArtistNames();
        ArtistDropDownList.DataSource = artists;
        ArtistDropDownList.DataValueField = "ArtistKey";
        ArtistDropDownList.DataTextField = "ArtistName";
        ArtistDropDownList.DataBind();
    }
    protected void Fill_Show_Dropdown()
    {
        int venueKey = Convert.ToInt32(Session["Userkey"]);
        GetterService.ShowsPerVenue[] shows = firstServices.GetVenueShows(venueKey);
        ShowsDropDownList.DataSource = shows;
        ShowsDropDownList.DataValueField = "VenueShowKey";
        ShowsDropDownList.DataTextField = "VenueShowName";
        ShowsDropDownList.DataBind();
    }
}