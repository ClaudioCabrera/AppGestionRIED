

using AppGestionRIED.services;
using AppGestionRIED.serviciosRemotos;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;

namespace AppGestionRIED
{
	public class PPrincipal : ContentPage
	{
        //Variables y objetos globales 
        Entry ent_texto_scan            = new Entry();
        Button btn_buscar_codigo        = new Button();
        Button btn_scan                 = new Button();
        Button btn_reportes             = new Button();
        //Datos a mostrar despues de escanear
        Label lbl_descripcion           = new Label();
        Label lbl_descripcion_det       = new Label();
        Label lbl_marca                 = new Label();
        Label lbl_modelo                = new Label();
        Label lbl_serie                 = new Label();
       // Label lbl_estado_fisico         = new Label();
        Button btn_editar               = new Button();
        int rut_usuario;
        

        


        //Clase de servicios remotos
        CServiciosRemotos consultaRemota = new CServiciosRemotos();


        public PPrincipal (int rut)
		{
            lbl_descripcion.TextColor = Color.Black;
            lbl_descripcion_det.TextColor = Color.Black;
            lbl_marca.TextColor = Color.Black;
            lbl_modelo.TextColor = Color.Black;
            lbl_serie.TextColor = Color.Black;
          //  lbl_estado_fisico.TextColor = Color.Black;


            //asigna valor de usuario
            rut_usuario = rut;
            //Establece el color de background a blanco
            BackgroundColor = Color.White;
            //logo
            Image img_logo = new Image();
            img_logo.Source = "logo_unap2.png";

            //texto del scan
            ent_texto_scan.Placeholder = "Escriba el código";
            ent_texto_scan.Keyboard = Keyboard.Numeric;
            

            //Boton para buscar
            btn_buscar_codigo.Text = "Buscar código";
            btn_buscar_codigo.Clicked += Btn_buscar_codigo_Clicked;


            //Boton para escanear
            btn_scan.Text = "Escanear código de barra o QR";
            btn_scan.Clicked += btn_scan_Clicked;

            //Boton para ver informes
            btn_reportes.Text = "VER HISTORIAL DE REPORTES";
            btn_reportes.Clicked += btn_reportes_Clicked;
            btn_reportes.IsVisible = false;
            

            //Editor
            //var editor_información = new Editor { Text="Información de recurso"};
            
           
            btn_editar.Text = "AGREGAR REPORTE";
            btn_editar.Clicked += Btn_editar_Clicked;
            btn_editar.IsVisible = false;
            //Servidor
            //  ent_servidor.Text = "app-activos-fijos.herokuapp.com";
            //  ent_servidor.IsVisible = false;


            Content = new StackLayout {
                Padding = new Thickness(20, 30, 20, 20),
                Children = {
                   
                    img_logo,
                    ent_texto_scan,
                    btn_buscar_codigo,
                    btn_scan,
                    lbl_descripcion,
                    lbl_descripcion_det,
                    lbl_marca,
                    lbl_modelo,
                    lbl_serie,
                    //lbl_estado_fisico,//---> B & M
                    btn_editar,
                    btn_reportes,
					
				}
			};
		}
        //Fin del constructor

        private void btn_reportes_Clicked(object sender, EventArgs e)
        {
            string codigo_barra = ent_texto_scan.Text;
            Navigation.PushModalAsync(new PReportes(codigo_barra));
        }

        private void Btn_buscar_codigo_Clicked(object sender, EventArgs e)
        {
            var result = ent_texto_scan.Text;
            if (result != null)
            {

                consultarDatosServidorElemento(result.ToString());
            }
        }

        void Btn_editar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PEditar(rut_usuario,lbl_descripcion.Text.ToString(),ent_texto_scan.Text.ToString()));
            
        }

        async void btn_scan_Clicked(object sender, EventArgs e)
        {
            // String servidor = ent_servidor.Text;
            // Navigation.PushAsync(new salida.PNuevaSalida(servidor));
            var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            if (result != null)
            {
                ent_texto_scan.Text = result;
                await CrossMediaManager.Current.Play("http://sonrais.com/activos_fijos/ProcessingR2D2.mp3");
            }
            consultarDatosServidorElemento(result.ToString());
          

        }

        async void consultarDatosServidorElemento(string codigo_barra)
        {
            var respuesta_remota = consultaRemota.obtenerDatosElemento(codigo_barra);
            //Alerta de prueba
            //await DisplayAlert("TEST","Datos respuesta"+ respuesta_remota ,"OK");
            var datosElemento = JArray.Parse(respuesta_remota);
            /*Cuenta la cantidad de datos recibidos*/
            int cantidad_datos_recibidos = datosElemento.Count;

            if (cantidad_datos_recibidos == 0)
            {
                await DisplayAlert("AVISO", "No se encontraron datos para el código de barras escaneado", "OK");
                //ent_codigo_barra.Text = "";
            }
            else
            {
                // CrossNotifications.Current.Vibrate(100);
                

                mostrarDatosElemento(datosElemento);


            }
        }

        private void mostrarDatosElemento(JArray datosElemento)
        {
            

            string r_descripcion        = datosElemento[0]["DESCRIPCION"].ToString();
            string r_descripcion_det    = datosElemento[0]["DESCRIPCION_DET"].ToString();
            string r_marca              = datosElemento[0]["MARCA"].ToString();
            string r_modelo             = datosElemento[0]["MODELO"].ToString();
            string r_serie              = datosElemento[0]["SERIE"].ToString();
           // string r_estado_fisico      = datosElemento[0]["ESTADO_FISICO"].ToString();
         

            //mostrar datos de recurso
            lbl_descripcion.Text         = "Nombre: "+r_descripcion;
            lbl_descripcion_det.Text     = "Descripción: "+r_descripcion_det;
            lbl_marca.Text               = "Marca: "+r_marca;
            lbl_modelo.Text              = "Modelo: "+r_modelo;
            lbl_serie.Text               = "Serie: "+r_serie;
            //  lbl_estado_fisico.Text       = "Estado físico: "+r_estado_fisico;

            btn_scan.IsVisible = false;
            btn_buscar_codigo.IsVisible = false;
            btn_editar.IsVisible = true;
            btn_reportes.IsVisible = true;
         
        }
    }
}