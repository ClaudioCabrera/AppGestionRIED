using AppGestionRIED.serviciosRemotos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

using Xamarin.Forms;

namespace AppGestionRIED
{
    public class PLogin : ContentPage
	{
        /**
         * 
         * ent_clave.Text = "T3rr1bl3";
         * ent_rut.Text = "12079126";
         * 
         * **/
        Entry ent_rut = new Entry();
        Entry ent_clave= new Entry();
        CServiciosRemotos consultaremota = new CServiciosRemotos();

        public PLogin ()
		{   
            //Desactiva la pagina de navegación superior
            NavigationPage.SetHasNavigationBar(this, false);
            //Establece el color de background a blanco
            BackgroundColor = Color.White;
                
            //Descripcion
            var lbl_titulo = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = "Gestión de recursos de infraestructura y \n equipamiento docente",
                TextColor = Color.FromHex("054E8F"),
                HorizontalTextAlignment = TextAlignment.Center
            };
            //logo
            Image img_logo = new Image();
            img_logo.Source = "logo_unap.png";

            //Label instrucciones
            Label lbl_instrucciones = new Label();
            lbl_instrucciones.HorizontalTextAlignment = TextAlignment.Center;
            lbl_instrucciones.Text = "Ingrese datos";
            lbl_instrucciones.TextColor = Color.Black;

            //Entry para usuario
            ent_rut.Placeholder = "Ingrese rut";
            ent_rut.Keyboard= Keyboard.Numeric;

            //Entry para clave
            ent_clave.Placeholder = "Ingrese clave";
            ent_clave.IsPassword = true;
            

            //Button para ingresar Login
         
            Button btn_login = new Button();
            btn_login.Text = "Ingresar";
            btn_login.Clicked += Btn_Login_Clicked;

            Content = new StackLayout {
                Padding = new Thickness(20, 30, 20, 20),
                Children = {

                    img_logo,
					lbl_titulo,
                    ent_rut,
                    ent_clave,
                    btn_login 
				}
			};
		}

        private void Btn_Login_Clicked(object sender, EventArgs e)
        {
            //variables que almacenan los valores de los Entry´s
            string rut = ent_rut.Text;
            string clave = ent_clave.Text;

            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(rut))
            //if (ent_rut.Text == null || ent_clave == null)
            {
                DisplayAlert("Aviso", "Credenciales Vacias", "OK");

            }
            else
            {
                int rut_envio = Convert.ToInt32(rut);
                string respuesta_remota = consultaremota.validarCredenciales(rut_envio, clave);
                //Transformamos los datos en objeto JObject. Requiere Newtonsoft JSON
                JObject datos_autentificacion = JObject.Parse(respuesta_remota);
                string r_entra = datos_autentificacion["ENTRA"].ToString();

                if (r_entra.Equals("SI"))
                {
                    Navigation.PushModalAsync(new PPrincipal(rut_envio));
                }
                else
                {
                    DisplayAlert("Aviso", "Credenciales incorrectas", "OK");
                }

            }
        }
    }
}