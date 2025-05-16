using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfAgendapoo
{
    public partial class frmTask: Form
    {
        DatabaseCruds db = new DatabaseCruds();

        public frmTask()
        {
            
            InitializeComponent();
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            DataTable table = db.getAllTasks();
            dgvTasks.DataSource = table;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskID.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                cmbPriority.SelectedIndex == -1 ||
                cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            string taskid = txtTaskID.Text.Trim();
            string description = txtDescription.Text.Trim();
            string priority = cmbPriority.SelectedItem.ToString();
            DateTime duedate = dtpDueDate.Value;
            string status = cmbStatus.SelectedItem.ToString();

            Task task = new Task(taskid, description, priority, duedate, status);

            try
            {
                db.addTask(task);
                MessageBox.Show("Tarea registrada correctamente.");
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskID.Text))
            {
                MessageBox.Show("Ingrese un ID de tarea para buscar.");
                return;
            }

            string taskid = txtTaskID.Text.Trim();
            Task task = db.searchTask(taskid);

            if (task != null)
            {
                txtDescription.Text = task.description;
                cmbPriority.SelectedItem = task.priority;
                dtpDueDate.Value = task.duedate;
                cmbStatus.SelectedItem = task.status;
            }
            else
            {
                MessageBox.Show("Tarea no encontrada.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskID.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                cmbPriority.SelectedIndex == -1 ||
                cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            string taskid = txtTaskID.Text.Trim();
            string description = txtDescription.Text.Trim();
            string priority = cmbPriority.SelectedItem.ToString();
            DateTime duedate = dtpDueDate.Value;
            string status = cmbStatus.SelectedItem.ToString();

            Task task = new Task(taskid, description, priority, duedate, status);

            try
            {
                db.updateTask(task);
                MessageBox.Show("Tarea actualizada correctamente.");
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskID.Text))
            {
                MessageBox.Show("Ingrese un ID de tarea para eliminar.");
                return;
            }

            string taskid = txtTaskID.Text.Trim();

            DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta tarea?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    db.deleteTask(taskid);
                    MessageBox.Show("Tarea eliminada correctamente.");
                    limpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            DataTable table = db.getAllTasks();
            dgvTasks.DataSource = table;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txtTaskID.Clear();
            txtDescription.Clear();
            cmbPriority.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            dtpDueDate.Value = DateTime.Now;
            txtTaskID.Focus();
        }
    }
}
