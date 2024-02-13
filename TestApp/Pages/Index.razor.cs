using System.Text.Json;
using Microsoft.AspNetCore.Components;
using TestApp.Models;

namespace TestApp.Pages
{
    public partial class Index
    {
        private readonly List<int> Years = new List<int>() { 2024, 2023, 2022, 2021, 2020 };
        private readonly Dictionary<string, string> Countries = new Dictionary<string, string>()
        {
            { "Russia", "ru" },
            { "USA", "us" },
            { "Belgium", "be" }
        };

        private DateTime startDate;
        private DateTime endDate;
        private List<PublicHolidayV3Dto> holidays;
        private int selectedYear;
        private string selectedCountry;


        [Inject]
        private HttpClient HttpClient { get; set; }

        protected override void OnInitialized()
        {
            selectedYear = DateTime.UtcNow.Year;
            selectedCountry = Countries.FirstOrDefault().Value;

            SetDateRange();
            _ = GetPublicHolidaysAsync();

        }

        private async Task GetPublicHolidaysAsync()
        {
            var response = await HttpClient.GetStringAsync($"https://date.nager.at/api/v3/PublicHolidays/{selectedYear}/{selectedCountry}");

            if (!string.IsNullOrEmpty(response))
            {
                holidays = JsonSerializer.Deserialize<List<PublicHolidayV3Dto>>(response) ?? new List<PublicHolidayV3Dto>();

                StateHasChanged();
            }
        }

        private void OnChangedYear(ChangeEventArgs args)
        {
            if (args.Value != null && int.TryParse(args.Value.ToString(), out int result))
            {
                selectedYear = result;

                SetDateRange();
                _ = GetPublicHolidaysAsync();
            }
        }

        private void OnChangedCountry(ChangeEventArgs args)
        {
            if (args.Value != null)
            {
                selectedCountry = args.Value.ToString();

                _ = GetPublicHolidaysAsync();
            }
        }

        private void SetDateRange()
        {
            startDate = new DateTime(selectedYear, 1, 1);
            endDate = new DateTime(selectedYear, 12, 31);
        }
    }
}