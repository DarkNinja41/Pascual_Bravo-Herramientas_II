using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerPractica1
{
    public abstract class ConductorMetro
    {
        public string Nombre { get; set; }
        public int Turno { get; set; }
        public int HorasTrabajadas { get; set; }

        protected ConductorMetro(string nombre, int turno, int horasTrabajadas) 
        { 
            Nombre = nombre;
            Turno = turno;
            HorasTrabajadas = horasTrabajadas;
        }

        //Método para calcular el salario
        public virtual double CalcularSalario()
        {
            return HorasTrabajadas * 1423500;
        }

        //Metodo para mostrar los datos
        public abstract void MostrarDatosConduct();
    }
}
