using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGestionRIED.serviciosRemotos;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AppGestionRIED
{
	public class PReportes : ContentPage
	{
        Label lbl_idcomentario = new Label();
        Label lbl_comentario = new Label();
      //  Label lbl_activo = new Label();
        Label lbl_idusuario = new Label();
        Label lbl_fecha = new Label();
        
        Image img_imagen_reporte = new Image();
        Image img_imagen_reporte2 = new Image();
        Image img_imagen_reporte3 = new Image();
        Image img_imagen_reporte4 = new Image();
        Image img_imagen_reporte5 = new Image();
        Image img_imagen_reporte6 = new Image();
        Image img_imagen_reporte7 = new Image();
        Image img_imagen_reporte8 = new Image();
        Image img_imagen_reporte9 = new Image();
        Image img_imagen_reporte10 = new Image();

        Image img_error = new Image();
        Image img_logo = new Image();
        Button btn_cargarReportes = new Button();
        string codigo_barra_global;
        CServiciosRemotos consultaRemota = new CServiciosRemotos();
        
		public PReportes (string codigo_barra)
		{
            img_error.IsVisible = false;
            img_logo.Source = "logo_unap2.png";
            lbl_idcomentario.TextColor = Color.Black;
            lbl_comentario.TextColor = Color.Black;
            lbl_idusuario.TextColor = Color.Black;
            lbl_fecha.TextColor = Color.Black;

            BackgroundColor = Color.White;
            codigo_barra_global = codigo_barra;
            btn_cargarReportes.Text = "Cargar reportes";
           // cargaReportes(codigo_barra_global);
            btn_cargarReportes.Clicked += Btn_cargarReportes_Clicked;

            /**
            Label header = new Label
            {
                Text = "Reportes",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            **/

            /**
            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = "http://jupiter.unap.cl/SV2/public/mobile-unico/nomina-comentarios-activo.php"
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
           **/

            
            
            Content = new StackLayout()//StackLayout
            {
               
               Padding = new Thickness(10, 20, 10, 10),

                
                Children = {

                new ScrollView {
                    Content = new StackLayout
                    {

                        Children = {
                            img_logo,
                            btn_cargarReportes,
                            
                            lbl_idusuario,
                            lbl_comentario,
                            lbl_fecha,
                            img_imagen_reporte,
                            img_imagen_reporte2,
                            img_imagen_reporte3,
                            img_imagen_reporte4,
                            img_imagen_reporte5,
                            img_imagen_reporte6,
                            img_imagen_reporte7,
                            img_imagen_reporte8,
                            img_imagen_reporte9,
                            img_imagen_reporte10,
                            img_error
                            
                            
                        }
                    }
                }
                    //header,
					//webView
                 //   btn_cargarReportes,
                 //   lbl_idcomentario,
                 //   lbl_comentario,
                 //   lbl_idusuario,
                 //   lbl_fecha,
                 //   img_imagen_reporte
                    
				}
			};
            

		}//Fin del constructor

        private void Btn_cargarReportes_Clicked(object sender, EventArgs e)
        {
            cargaReportes(codigo_barra_global);
        }

        async void cargaReportes(string codigo_barra)
        {
            var respuesta_remota = consultaRemota.obtenerReportes(codigo_barra);
           // await DisplayAlert("TEST", "Datos respuesta" + respuesta_remota, "OK");
            var datosReportes = JArray.Parse(respuesta_remota);
            int cantidad_datos_recibidos = datosReportes.Count;

            if (cantidad_datos_recibidos == 0)
            {
                await DisplayAlert("AVISO", "No se encontraron datos para el código de barras escaneado", "OK");
                
            }
            else
            {
                // CrossNotifications.Current.Vibrate(100);


                mostrarReportes(datosReportes);


            }
        }

        private void mostrarReportes(JArray datosReportes)
        {
            /**
                string r_idcomentario = datosReportes[0]["idcomentario"].ToString();
                string r_comentario = datosReportes[0]["comentario"].ToString();
                string r_idusuario = datosReportes[0]["idusuario"].ToString();
                string r_fecha = datosReportes[0]["fecha"].ToString();
                string r_imagen = datosReportes[0]["url_imagen"].ToString();

                lbl_idcomentario.Text = "ID de Comentario: " + r_idcomentario;
                lbl_comentario.Text = "Comentario: " + r_comentario;
                lbl_idusuario.Text = "ID de Usuario: " + r_idusuario;
                lbl_fecha.Text = "Fecha: " + r_fecha;
                img_imagen_reporte.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri(r_imagen) };
           **/
            var v_datosreportes = datosReportes;

            int i = 0;
            foreach ( JObject obj in v_datosreportes)
            {
                string r_idcomentario = datosReportes[i]["idcomentario"].ToString();
                string r_comentario = datosReportes[i]["comentario"].ToString();
                string r_idusuario = datosReportes[i]["idusuario"].ToString();
                string r_fecha = datosReportes[i]["fecha"].ToString();
                string r_imagen = datosReportes[i]["url_imagen"].ToString();

                lbl_idcomentario.Text += "ID de Comentario: " + r_idcomentario+"\n";
                lbl_comentario.Text += "Reporte: " + r_comentario+"\n";
                lbl_idusuario.Text += "ID de Usuario: " + r_idusuario+"\n";
                lbl_fecha.Text += "Fecha de reporte: " + r_fecha+"\n";

                switch (i)
                {
                    case 0:
                        img_imagen_reporte.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                   break;

                    case 1:
                        img_imagen_reporte2.Source = new UriImageSource
                            {CachingEnabled = false, Uri= new Uri(r_imagen) };
                        break;
                    case 2:
                        img_imagen_reporte3.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 3:
                        img_imagen_reporte4.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 4:
                        img_imagen_reporte5.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 5:
                        img_imagen_reporte6.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 6:
                        img_imagen_reporte7.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 7:
                        img_imagen_reporte8.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 8:
                        img_imagen_reporte9.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    case 9:
                        img_imagen_reporte10.Source = new UriImageSource
                        { CachingEnabled = false, Uri = new Uri(r_imagen) };
                        break;
                    default:
                        img_error.Source = "error.png";
                        img_error.IsVisible = true;
                        break;

                }

                
               
                i++;  
                }
                

            }


        }
    }


    
