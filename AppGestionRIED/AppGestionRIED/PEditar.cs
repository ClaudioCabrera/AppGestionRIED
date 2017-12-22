using AppGestionRIED.serviciosRemotos;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGestionRIED
{
    public class PEditar : ContentPage
    {
        //Variables y objetos globales

        Label lbl_descripcion = new Label();
        Button btn_agregar_observacion = new Button();
        Label lbl_comentario = new Label();
        Entry ent_comentario = new Entry();

        //Fotos
        MediaFile imagen;
        String rutaCompleta;
        Label lbl_ruta_foto = new Label();
        Label lbl_nombre_foto = new Label();
        Label lbl_resultado_url = new Label();
        Image img_foto_ried = new Image();
        Button btn_tomar_foto = new Button();

        string fecha_actual = DateTime.Now.ToString("yyyyMMddHmmss");
        string codigo_comentario;
        string codigo_activo;
        //Picker picker = new Picker();



        //string pick_estado_fisico;
        //string pick_estado_fisico_final;

        string codigo_comentario_final;//--->Es igual a la fecha actual + codigo de activo

        int rut_usuario;
        //Servicios remotos
        CServiciosRemotos consultaRemota = new CServiciosRemotos();


        public PEditar ( int rut__usuario,string descripcion, string codigo_barra)
		{
            //logo
            Image img_logo = new Image();
            img_logo.Source = "logo_unap2.png";

            BackgroundColor = Color.White;

            rut_usuario = rut__usuario;

            codigo_comentario = fecha_actual.ToString();
            codigo_activo = codigo_barra;
            codigo_comentario_final = codigo_comentario+""+codigo_activo;
          
            

            //Picker estado fisico
    /**        var estado_fisico = new List<string>();
            estado_fisico.Add("Bueno");
            estado_fisico.Add("Malo");
            picker = new Picker {Title="Selecciona un estado"};
            picker.ItemsSource = estado_fisico;

            pick_estado_fisico = estado_fisico.ToString();

            if (pick_estado_fisico.Equals("Bueno"))
            {
                pick_estado_fisico_final = "B";
            }
            else
            {
                pick_estado_fisico_final = "M";
            }
     **/
            //label descripcion
            lbl_descripcion.Text= descripcion;
            lbl_descripcion.TextColor = Color.Black;
           
            lbl_comentario.Text= "Realice un comentario acerca del estado de este recurso";
            lbl_comentario.TextColor = Color.Black;

            //Editor comentario
            // var edit_comentario = new Editor { Text= "Reporte:"};
            // comentario= edit_comentario.Text;
            ent_comentario.Placeholder = "Ingrese reporte";
           
            //Tomar foto
            btn_tomar_foto.Text = "Tomar foto";
            btn_tomar_foto.Image = "camara_48x48.png";
            btn_tomar_foto.Clicked += Btn_tomar_foto_Clicked;
            //Boton para agregar comentario
            btn_agregar_observacion.Text = "AGREGAR REPORTE";
            btn_agregar_observacion.Clicked += Btn_agregar_observacion_Clicked;

            /**
             * 
             * Debug.WriteLine("clicked");
             *  var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;

                    var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

                    Debug.WriteLine("Position Status: {0}", position.Timestamp);
                    Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                    Debug.WriteLine("Position Longitude: {0}", position.Longitude);
                    
                    var latitud_actual = position.Latitude.ToString();
                    var longitud_actual = position.Longitude.ToString();
             
             * 
             * **/
           ;
            Content = new StackLayout {
                Padding = new Thickness(20, 30, 20, 20),
                Children = {
                    new ScrollView{
                        Content = new StackLayout{
                     Children ={
                            img_logo,
                            lbl_descripcion,
                            lbl_comentario,
                            ent_comentario,
                   
                            btn_tomar_foto,
                            btn_agregar_observacion,
                            lbl_resultado_url,
                            img_foto_ried
                             }
                        }
                    }
                    
				}
			};
		}

        private async void Btn_tomar_foto_Clicked(object sender, EventArgs e)
        {
            try
            {
                String nombreFoto = lbl_descripcion.Text.ToString();

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No hay cámara", "La cámara no se encuentra disponible", "OK");
                    return;
                };

                imagen = await CrossMedia.Current.TakePhotoAsync(
                    new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        //SaveToAlbum = true,
                        Directory = "ReportesImagenes",
                        Name = codigo_comentario +""+codigo_activo+".jpg",
                    });

                if (imagen == null)
                    return;
                //lbl_ruta_foto.Text = imagen.;
                rutaCompleta = imagen.Path;
                lbl_ruta_foto.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));

                img_foto_ried.Source = ImageSource.FromStream(() =>
                {
                    var stream = imagen.GetStream();
                    imagen.Dispose();
                    return stream;
                });

                img_foto_ried.Source = ImageSource.FromFile(rutaCompleta);

                string respuesta_imagen = consultaRemota.enviarImagenServidor(nombreFoto, rutaCompleta);
                //await DisplayAlert("Aviso", "Error"+respuesta_imagen, "OK");
                JObject datos_upload_imagen = JObject.Parse(respuesta_imagen);
                
               int respuesta_upload_imagen = Convert.ToInt32(datos_upload_imagen["RESULTADO"].ToString());


                await Task.Delay(1000);



                if (respuesta_upload_imagen == 1)
                {
                   //  consultaRemota.enviarImagenServidorSQLSERVER(rut, rutaCompleta);  
                    lbl_resultado_url.Text = "La foto se ha registrado correctamente!";
                    lbl_resultado_url.TextColor = Color.Green;
                }
                else
                {
                    lbl_resultado_url.Text = "Error al subir imagen";
                    lbl_resultado_url.TextColor = Color.Red;
                }

                await DisplayAlert("Aviso", "La foto se ha registrado correctamente!", "OK");
    

            } 
            
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Se ha producido un problema " + ex.Message, "OK");
            }

        }

        private async void Btn_agregar_observacion_Clicked(object sender, EventArgs e)
        {


            

            //Cambiar estado de activo B o M
  /**          string respuesta_cambio_estado = consultaRemota.cambiarEstado(pick_estado_fisico_final);
            await DisplayAlert("Aviso", "Error"+respuesta_cambio_estado,"Ok");
            JObject datos_cambio_estado = JObject.Parse(respuesta_cambio_estado);
            int respuesta_upload_cambio_estado = Convert.ToInt32(datos_cambio_estado["RESULTADO"].ToString());

            if (respuesta_upload_cambio_estado == 1)
            {
                await DisplayAlert("Aviso", "Cambio de estado correcto", "OK");
            }
            else
            {
                await DisplayAlert("Aviso", "Error" + respuesta_upload_cambio_estado, "OK");
            }
    **/

            //Agregar comentarios
            string comentario = ent_comentario.Text;
            string respuesta_comentario= consultaRemota.agregarComentario(codigo_comentario_final,comentario,rut_usuario,codigo_activo);//Elimine picker
            
          //  await DisplayAlert("Aviso", "Error" + respuesta_comentario, "OK");
            JObject datos_comentario = JObject.Parse(respuesta_comentario);
            int respuesta_upload_comentario = Convert.ToInt32(datos_comentario["RESULTADO"].ToString());

            if (respuesta_upload_comentario == 1)
            {
                
                lbl_resultado_url.Text = "El comentario se ha registrado correctamente!";
                lbl_resultado_url.TextColor = Color.Green;
            }
            else
            {
                lbl_resultado_url.Text = "Error al agregar comentario";
                lbl_resultado_url.TextColor = Color.Red;
            }

            await DisplayAlert("Aviso", "Comentario registrado correctamente!", "OK");
        }
    }
}