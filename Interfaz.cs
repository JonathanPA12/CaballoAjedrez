using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CaballoAjedrez
{
    public partial class Interfaz : Form
    {
        int[,] juegoSolucion = new int[8, 8];
        int[,] juegoPasos = new int[8, 8];
        int paso = 1;
        int fila = 8;
        int columna = 8;
        int[] linea = new int[64];
        

        public Interfaz()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Caballo de Ajedrez";
            //referencia del diseño en https://www.chess.com/game/live/60275471315
            Cargar_Archivo();
            Crear_Solucion();



        }


        private void Form1_Load(object sender, EventArgs e){}
        private void pasos()
        {
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    if (juegoSolucion[i, j] == paso)
                    {
                        juegoPasos[i, j] = juegoSolucion[i, j];
                        paso++;
                    }
                }
            }
        }
        private void Crear_Solucion()
        {
            int contador = 0;
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    juegoSolucion[i, j] = linea[contador];
                    contador++;
                }
            }
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    juegoPasos[i, j] = 0;
                }
            }
        }
        private void Cargar_Archivo()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"..//..//..//BackendCaminoCaballo.txt"); //la ruta del archivo
            String x = file.ReadToEnd();
            String[] vector = x.Split(new char[] { ' ', ',','-' }); // separa el string en un vector de strings por cada espacio o coma que encuentre en el string x
            int cont = 0;
            foreach (String str in vector)
            {
                linea[cont] = Convert.ToInt32(str);
                cont++;
            }
        }
     
 

        // boton para reiniciar el juego
        private void button3_Click(object sender, EventArgs e)
        {
            Label label;
            for (int i = 63; i >= 0; i--)
            {
                label = tableLayoutPanel1.Controls[i] as Label;
                label.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                label.Text = " ";
            }
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    juegoPasos[i, j] = 0;
                    paso = 1;
                }
            }
        }
        //boton para ver la solucion
        private void button2_Click(object sender, EventArgs e)
        {
            Label label;
            int campos = 63;
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    label = tableLayoutPanel1.Controls[campos] as Label;
                    label.Text = Convert.ToString(juegoSolucion[i, j]);
                    label.ForeColor = Color.Black;
                    label.Font = new Font("Segoe UI", 20); // tamaño de la letra
                    campos--;
                }
            }
        }
        //boton de movimiento 
        private void button1_Click(object sender, EventArgs e)
        {
            Label label; //variable de tipo label para poder crear los labels
            int indice = 63; //tamaño del tablero -1

            pasos();

            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    if (juegoPasos[i, j] != 0) // lee el contenido de la matriz de pasos
                    {
                        label = tableLayoutPanel1.Controls[indice] as Label; //crea un label en la posicion del indice
                        label.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                        label.Text = "♞";
                        label.ForeColor = Color.Black; // pasa el caballito a negro 
                    }
                    indice--; // se va disminuyendo comforme se va llenando el tablero
                }
            }

        }
        //boton para salir del juego
        private void button4_Click(object sender, EventArgs e)
        {
            //cerrar y salir de juego
          
            Application.Exit(); 

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog(); //encontrar el archivo del backend

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                string direccion = abrir.FileName;

                Process proceso = new Process();
                proceso.StartInfo.FileName = direccion;
            }
        }
    }
}
