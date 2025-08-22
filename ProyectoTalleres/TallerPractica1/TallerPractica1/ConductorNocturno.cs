using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerPractica1
{
    public abstract class ConductorNocturno : ConductorMetro
    {
        private double RecargoNoct {  get; set; }

        public ConductorNocturno(string nombre, int turno, int horasTrabajadas, double recargoNoct)
            : base(nombre, turno, horasTrabajadas)
        {
            RecargoNoct = recargoNoct;
        }

        public double SalarioNoct()
        {
            return CalcularSalario() * 0.20;
        }

        public override double CalcularSalario()
        {
            return base.CalcularSalario()+;
        }
    }
}
