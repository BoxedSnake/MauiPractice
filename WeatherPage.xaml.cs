using MauiPractice.Services;
using Microsoft.Maui.Devices.Sensors;

namespace MauiPractice;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;

    public WeatherPage()
    {
        InitializeComponent();
        WeatherList = new List<Models.List>();

    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = await ApiService.GetWeather(47.6829, -122.1209);


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