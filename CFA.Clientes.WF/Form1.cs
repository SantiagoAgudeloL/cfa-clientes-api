using System.Net.Http;
using System.Text.Json;
using CFA.Clientes.WF.Models;

namespace CFA.Clientes.WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private async Task<List<Cliente>> ObtenerClientes()
        {
            using var client = new HttpClient();

            var url = "https://localhost:7251/api/clientes";

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        return JsonSerializer.Deserialize<List<Cliente>>(json,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            })!;
                    }
                }
                catch
                {

                }

                await Task.Delay(1000);
            }

            return new List<Cliente>();
        }

        private async void Form1_Load_1(object sender, EventArgs e)
        {
            var clientes = await ObtenerClientes();

            dgvClientes.DataSource = clientes;
        }
    }
}
