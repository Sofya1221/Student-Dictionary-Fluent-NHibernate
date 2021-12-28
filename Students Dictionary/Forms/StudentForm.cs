using NHibernate;
using StudentsDictionary.Repository.impl;
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
        private readonly StudentRepositoryImpl studentRepo;
        private readonly GroupRepositoryImpl groupRepo;
        public StudentForm()
        {
            ISession session = SessionFactory.OpenSession;
            this.studentRepo = new StudentRepositoryImpl(session);
            this.groupRepo = new GroupRepositoryImpl(session);
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
            IList<Model.Student> students = this.studentRepo.GetAll();
            studentsView.DataSource = students;

            IList<Model.Group> groups = this.groupRepo.GetAll();
            comboBox1.DataSource = groups;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool succsess = this.studentRepo.Delete(int.Parse(IdTxtBx.Text));
            if (succsess)
            {
                loadStudentData();

                IdTxtBx.Text = "";
                tFirstName.Text = "";
                tLastName.Text = "";
                tEmail.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Студент не найден!");
            }            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Model.Student student = new Model.Student();

            SetStudentInfo(student);

            this.studentRepo.Save(student);
            loadStudentData();
            
            tFirstName.Text = "";
            tLastName.Text = "";
            tEmail.Text = "";
           
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
            Model.Student student = this.studentRepo.GetById(int.Parse(IdTxtBx.Text));

            if (student != null)
            {
                SetStudentInfo(student);
                this.studentRepo.Save(student);

                loadStudentData();
                IdTxtBx.Text = "";
                tFirstName.Text = "";
                tLastName.Text = "";
                tEmail.Text = "";
            } else
            {
                System.Windows.Forms.MessageBox.Show("Студент не найден!");
            }
        }

        private void dgViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (studentsView.RowCount <= 1 || e.RowIndex < 0)
                return;
            string id = studentsView[0, e.RowIndex].Value.ToString();

            if (id == "")
                return;

            Model.Student student = this.studentRepo.GetById(int.Parse(id));

            IdTxtBx.Text = student.Id.ToString();
            tFirstName.Text = student.FirstName.ToString();
            tLastName.Text = student.LastName.ToString();
            tEmail.Text = student.Email.ToString();
            int index = comboBox1.Items.IndexOf(student.Group);
            if (index != -1)
            {
                comboBox1.SelectedIndex = index;
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
