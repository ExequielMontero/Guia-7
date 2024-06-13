using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1_Guia_7
{
    internal class Program
    {
        public static int i = 0;
        static void Main(string[] args)
        {
            string[] patentes = new string [25];
            int codigo = 0, codigo1 = 234145;
            double totalmaspordias = 0;
            double mayor = 0;
            int opcion, cant = 0, num = 0, cantpersonas = 0, cantvehiculos = 0;
            int ticket = 0, k=0;
            double recaudacion = 0;
            double totaltotal = 0;
            string patente = "", codigomayor = "";
            do
            {
                Console.Clear();
                opcion = menu();
                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("¿Tenes ticket? [1]Si [2]No [0]Salir");
                        ticket = int.Parse(Console.ReadLine());
                        if (ticket != 0)
                        {
                            if (ticket == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Ingrese su codigo de ticket");
                                codigo = int.Parse(Console.ReadLine());
                            }
                            else if (ticket == 2)
                            {
                                codigo++;
                                codigo = codigo1++;
                                Console.Clear();
                                Console.WriteLine("¡Su nuevo codigo te ticket generado es de " + codigo +"!");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine($"La recaudacion total al momento es de {recaudacion}$, y la ultima operacion fue la siguiente:\n");
                        if (recaudacion == 0)
                        {
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine($"Personas y Vehiculos en el lugar:\nPersonas en el lugar: {cantpersonas}\nVehiculos en el lugar {cantvehiculos}");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        imprimirpatentes(patentes);
                        Console.WriteLine("");
                        Console.WriteLine("Presione una tecla para seguir...");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Ingresar numero de patente a buscar en la base de datos:");
                        string q = Convert.ToString(Console.ReadLine());
                        encontrarpatente(patentes, q);
                        Console.ReadKey();
                        break;
                }


                if (opcion == 1 && ticket != 0)
                {
                    Console.Clear();
                    Console.WriteLine("Segun elegir: [0]No tenes vehiculo [1]Moto [2]Auto [3]Camioneta [4]Bugy [5]Vehiculo nautico");
                    num = int.Parse(Console.ReadLine());
                    Console.Clear();
                    if(num>0 && num < 6)                                //agregado
                    {
                        Console.WriteLine("Ingrese su patente:");
                        patente = Console.ReadLine();
                        basepatentes(patentes, patente);
                    }                                                               //fin agregado
                    cant = CantidadVehiculos(num);
                    if (num == 0)
                    {
                        cantpersonas += cant;
                    }
                    else if (num > 0 && num < 6)
                    {
                        cantvehiculos += cant;
                    }
                    double totalvehiculo1 = totaldinero(num, cant);
                    Console.WriteLine("Cuantos dias se van a quedar:");
                    int dias = int.Parse(Console.ReadLine());
                    Console.Clear();
                    totalmaspordias = porcantidaddedias(totalvehiculo1, dias);
                    totaltotal = ivamaspeces(totalmaspordias);
                }

                if (totaltotal > mayor)
                {
                    mayor = totaltotal;
                    codigomayor = patente;
                }

                if (opcion == 1 || opcion == 2)
                {
                    if(num==0)
                    {
                        if (k == 0)
                        {
                            Console.WriteLine($"Su numero de ticket {codigo}.\n{cant}  {tipo(num)} {totaldinero(num, cant),10:f3}$\n+Dias {totalmaspordias,18:f3}$\n+ IVA + Peces {totaltotal,10:f3}$\n\nAun no se ha registrado nadie con vehiculo.");
                        }
                        else
                        {
                            Console.WriteLine($"Su numero de ticket {codigo}.\n{cant}  {tipo(num)} {totaldinero(num, cant),10:f3}$\n+Dias {totalmaspordias,18:f3}$\n+ IVA + Peces {totaltotal,10:f3}$\n\nLa patente de mayor ganancia fue {codigomayor}, con un total de {mayor}$ presupuesto.");
                        }
                    }
                    else if (ticket == 1 || ticket==2 && opcion>0 && opcion<6)
                    {
                        Console.WriteLine($"Su numero de patente es {patente} ticket {codigo}.\n{cant}  {tipo(num)} {totaldinero(num, cant),10:f3}$\n+Dias {totalmaspordias,18:f3}$\n+ IVA + Peces {totaltotal,10:f3}$\n\nHasta ahora la patente de mayor ganancia fue {codigomayor}, con un total de {mayor}$ presupuesto.");
                        k++;
                    }
                    
                    if (ticket != 0)
                    {
                        Console.ReadKey();
                    }
                }
                recaudacion += totaltotal;

            } while (opcion != 0);

        }

        static int CantidadVehiculos(int num)
        {
            int cant = 0;

            if (num == 0)
            {
                Console.WriteLine("Ingrese cuanta cantidad de personas son:");
                cant = int.Parse(Console.ReadLine());
            }
            else if (num > 0 && num <= 5)
            {
                Console.WriteLine("Ingrese la cantidad de vehiculos:");
                cant = int.Parse(Console.ReadLine());
            }
            return cant;
        }

        public static double totaldinero(int num, int cant)
        {
            double totalvehiculo1 = 0;
            switch (num)
            {
                case 0:
                    totalvehiculo1 = cant * 100;
                    break;
                case 1:
                    totalvehiculo1 = cant * 800;
                    break;
                case 2:
                    totalvehiculo1 = cant * 1000;
                    break;
                case 3:
                    totalvehiculo1 = cant * 1500;
                    break;
                case 4:
                    totalvehiculo1 = cant * 5000;
                    break;
                case 5:
                    totalvehiculo1 = cant * 1200;
                    break;
            }
            return totalvehiculo1;
        }



        static double porcantidaddedias(double totalvehiculo1, int dias)
        {
            double porcentaje = 0;
            switch (dias)
            {
                case 1:
                    porcentaje = totalvehiculo1;
                    break;
                case 2:
                    porcentaje = (120 * totalvehiculo1) / 100;
                    break;
                case 3:
                    porcentaje = (220 * totalvehiculo1) / 100;
                    break;
                case 4:
                    porcentaje = (320 * totalvehiculo1) / 100;
                    break;
            }
            if (dias >= 5 && dias <= 10)
            {
                porcentaje = (380 * totalvehiculo1) / 100;
            }

            return porcentaje;
        }

        public static double ivamaspeces(double totalmaspordias)
        {
            double iva = (121 * totalmaspordias) / 100;
            double peces = (15 * totalmaspordias) / 100;

            double ivamaspeces = (iva + peces);

            return ivamaspeces;
        }


        static string tipo(int num)
        {
            string tipo = "";
            switch (num)
            {
                case 0:
                    tipo = "Persona";
                    break;
                case 1:
                    tipo = "Moto";
                    break;
                case 2:
                    tipo = "Auto";
                    break;
                case 3:
                    tipo = "Camioneta";
                    break;
                case 4:
                    tipo = "Bugy";
                    break;
                case 5:
                    tipo = "Vehiculo Nautico";
                    break;
            }
            return tipo;
        }
        static int menu()
        {
            Console.WriteLine("0 - otro - Salir");
            Console.WriteLine("1 - Verificar Acceso");
            Console.WriteLine("2 - Imprimir Recaudación");
            Console.WriteLine("3 - Mostrar cantidad de accesos");
            Console.WriteLine("4 - Imprimir listado de patentes");
            Console.WriteLine("5 - Buscar patente");
            int opcion = int.Parse(Console.ReadLine());
            return opcion;
        }

        public static void basepatentes(string[] patentes, string patente)
        {
            patentes[i] = patente;
            i++;
        }

        public static void imprimirpatentes(string[] patentes)
        {
            foreach(var i in patentes)
            {
                Console.WriteLine(i);
            }
        }

        static void encontrarpatente(string[] patentes, string patente)
        {
            int i = 0;
            int pos = -1;

            while(pos==-1 && i<patentes.Length) 
            {
                if (patentes[i] == patente)
                {
                    pos = i;

                    Console.WriteLine($"La patente fue encontrada con exito y esta en la posicion {i} de la lista y es la patente {patentes[i]}");
                }

                i++;
            }

            if (pos == -1)
            {
                Console.WriteLine("¡La patente no fue encontrada!");
            }

        }

    }   
}   
