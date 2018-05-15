using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AsusRouterApp.Class
{
    public class MainPageData
    {
        public Model.WANInfo wanInfo { get; set; }

        public Model.NetRate netRate { get; set; }

        public Model.CpuMemInfo cpuMemInfo { get; set; }

        public Model.Client clients { get; set; }

        public Dictionary<string, Model.DeviceRate> devRate { get; set; }

        public Model.WLANInfo wlanInfo { get; set; }

        public Model.NetSpeed netSpeed { get; set; }

        public string[] banList { get; set; }

        public MainPageData() { }

        public async Task<bool> ExportDemoData()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this);
                var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
                savePicker.FileTypeChoices.Add("Json", new List<string>() { ".json" });
                savePicker.SuggestedFileName = "DemoData";
                var file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    await FileIO.WriteTextAsync(file, json);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static async Task<MainPageData> GetDemoData()
        {
            try
            {
                var json = await PathIO.ReadTextAsync("ms-appx:///Assets/DemoData.json");
                var data= JsonConvert.DeserializeObject<MainPageData>(json);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
