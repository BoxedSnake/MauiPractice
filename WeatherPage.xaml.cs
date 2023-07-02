using MauiPractice.Services;
using Microsoft.Maui.Devices.Sensors;

namespace MauiPractice;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longtitude;
    public WeatherPage()
    {
        InitializeComponent();
        WeatherList = new List<Models.List>();

    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longtitude);

    }


    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        longtitude = location.Longitude;
        latitude = location.Latitude;

    }

    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longtitude);
    }

    public async Task GetWeatherDataByLocation(double  latitude, double longitude)
    {
        var result = await ApiService.GetWeather(latitude, longtitude);
        UpdateUI(result);


    }
    
    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);

        UpdateUI(result);

    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
       var result = await DisplayPromptAsync(
            title: "", 
            message: "", 
            placeholder: "Search weather by city",
            accept: "Search",
            cancel: "Cancel"
            );
        if( result != null)
        {
            await GetWeatherDataByCity( result );

        }

    }

    public void UpdateUI(dynamic result)
    {
        foreach (var List in result.list)
        {
            WeatherList.Add(List);
        };
        CvWeather.ItemsSource = WeatherList;

        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblCity.Text = result.city.name;
        LblTemperature.Text = result.list[0].main.temperature + "°C";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + " km/hr";
        ImgWeatherIcon.Source = result.list[0].weather[0].fullIconUrl;
    }

}