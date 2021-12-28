using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDictionary.Forms
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            loadStudentData();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            loadStudentData();
        }

        private void loadStudentData()
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                IQuery query = session.CreateQuery("FROM Student");
                IList<Model.Student> students = query.List<Model.Student>();
                studentsView.DataSource = students;

                IQuery query1 = session.CreateQuery("FROM Group");
                comboBox1.DataSource = query1.List<Model.Group>().ToList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = session.CreateQuery("FROM Student WHERE Id = '" + IdTxtBx.Text + "'");
                        Model.Student student = query.List<Model.Student>()[0];
                        session.Delete(student);
                        transaction.Commit();

                        loadStudentData();

                        IdTxtBx.Text = "";
                        tFirstName.Text = "";
                        tLastName.Text = "";
                        tEmail.Text = "";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Model.Student student = new Model.Student();

            SetStudentInfo(student);

            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(student);
                        transaction.Commit();
                        loadStudentData();

                        tFirstName.Text = "";
                        tLastName.Text = "";
                        tEmail.Text = "";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        private void SetStudentInfo(Model.Student student)
        {
            student.FirstName = tFirstName.Text;
            student.LastName = tLastName.Text;
            student.Email = tEmail.Text;
            student.Group = (Model.Group) comboBox1.SelectedItem;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //to update data we will load current data to our textbox and then update
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = session.CreateQuery("FROM Student WHERE Id = '" + IdTxtBx.Text + "'");
                        Model.Student student = query.List<Model.Student>()[0];
                        SetStudentInfo(student);
                        session.Update(student);
                        transaction.Commit();

                        loadStudentData();

                        IdTxtBx.Text = "";
                        tFirstName.Text = "";
                        tLastName.Text = "";
                        tEmail.Text = "";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        private void dgViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (studentsView.RowCount <= 1 || e.RowIndex < 0)
                return;
            string id = studentsView[0, e.RowIndex].Value.ToString();

            if (id == "")
                return;

            IList<Model.Student> student = getDataFromStudent(id);

            IdTxtBx.Text = student[0].Id.ToString();
            tFirstName.Text = student[0].FirstName.ToString();
            tLastName.Text = student[0].LastName.ToString();
            tEmail.Text = student[0].Email.ToString();
            int index = comboBox1.Items.IndexOf(student[0].Group);
            if (index != -1)
            {
                comboBox1.SelectedIndex = index;
            }
        }

        private IList<Model.Student> getDataFromStudent(string id)
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        IQuery query = session.CreateQuery("FROM Student WHERE Id = '" + id+ "'");
                        return query.List<Model.Student>();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
