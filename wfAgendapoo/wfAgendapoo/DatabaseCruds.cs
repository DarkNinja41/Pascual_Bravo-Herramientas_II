using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfAgendapoo
{
    class DatabaseCruds
    {
        //Variable global para la conexión a la base de datos
        public string connDB = "Server=.;Database=Agenda;Trusted_Connection=True;";
        
        //Recuperar las tareas
        public List<Task> getTask()
        {
            List<Task> tasks = new List<Task>();
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM task", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new Task(
                        (string)reader["taskid"],
                        (string)reader["description"],
                        (string)reader["priority"],
                        (DateTime)reader["duedate"],
                        (string)reader["status"]
                    ));
                }
            }
            return tasks;
        }

        //Buscar tarea por ID
        public List<Task> getTaskById(string taskid)
        {
            List<Task> tasks = new List<Task>();
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM task WHERE taskid = @taskid", conn);
                cmd.Parameters.AddWithValue("@taskid", taskid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new Task(
                        (string)reader["taskid"],
                        (string)reader["description"],
                        (string)reader["priority"],
                        (DateTime)reader["duedate"],
                        (string)reader["status"]
                    ));
                }
            }
            return tasks;
        }

        //Agregar tarea
        public void addTask(Task task)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO task VALUES (@taskid, @description, @priority, @duedate, @status)", conn);
                cmd.Parameters.AddWithValue("@taskid", task.taskid);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@priority", task.priority);
                cmd.Parameters.AddWithValue("@duedate", task.duedate);
                cmd.Parameters.AddWithValue("@status", task.status);
                cmd.ExecuteNonQuery();
            }
        }

        //Actualizar tarea
        public void updateTask(string oldid, Task task)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE task SET taskid=@taskid, description=@description, priority=@priority, duedate=@duedate, status=@status WHERE taskid=@oldid", conn);
                cmd.Parameters.AddWithValue("@taskid", task.taskid);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@priority", task.priority);
                cmd.Parameters.AddWithValue("@duedate", task.duedate);
                cmd.Parameters.AddWithValue("@status", task.status);
                cmd.Parameters.AddWithValue("@oldid", oldid);
                cmd.ExecuteNonQuery();
            }
        }

        //Eliminar tarea
        public void deleteTask(string taskid)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM task WHERE taskid = @taskid", conn);
                cmd.Parameters.AddWithValue("@taskid", taskid);
                cmd.ExecuteNonQuery();
            }
        }

        public Task searchTask(string taskid)
        {
            Task task = null;
            string sql = "SELECT * FROM tarea WHERE taskid = @taskid";

            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@taskid", taskid);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string description = reader["description"].ToString();
                    string priority = reader["priority"].ToString();
                    DateTime duedate = Convert.ToDateTime(reader["duedate"]);
                    string status = reader["status"].ToString();

                    task = new Task(taskid, description, priority, duedate, status);
                }

                con.Close();
            }

            return task;
        }

        public void updateTask(Task task)
        {
            string sql = "UPDATE tarea SET description = @description, priority = @priority, duedate = @duedate, status = @status WHERE taskid = @taskid";

            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@taskid", task.taskid);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@priority", task.priority);
                cmd.Parameters.AddWithValue("@duedate", task.duedate);
                cmd.Parameters.AddWithValue("@status", task.status);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public DataTable getAllTasks()
        {
            DataTable table = new DataTable();
            string sql = "SELECT * FROM task";

            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }

            return table;
        }
        private SqlConnection getConnection()
        {
            string connectionString = "Data Source=.;Initial Catalog=Agenda;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }
    }
}
