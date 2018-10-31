using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeWebClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            EmployeeService.EmployeeServiceClient client = new
                EmployeeService.EmployeeServiceClient();

            EmployeeService.Employee employee = client.GetEmployee(Convert.ToInt32(txtId.Text));
            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DateOfBirth.ToShortDateString();
            DropDownList1.SelectedValue = ((int)employee.Type).ToString();
            if(employee.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtMonthlySalary.Text = ((EmployeeService.FullTimeEmployee)employee).MonthlySalary.ToString();
                trMonthlySalary.Visible = true;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text = ((EmployeeService.PartTimeEmployee)employee).HourlyPay.ToString();
                txtHoursWorked.Text = ((EmployeeService.PartTimeEmployee)employee).HoursWorked.ToString();
                trMonthlySalary.Visible = false;
                trHourlyPay.Visible = true;
                trHoursWorked.Visible = true;
            }

            lblMessage.Text = "Employee retrieved";
        }

        protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedValue == "-1")
            {
                lblMessage.Text = "Please select Employee Type";
            }
            else
            { 
                EmployeeService.EmployeeServiceClient client = new
                    EmployeeService.EmployeeServiceClient();
                EmployeeService.Employee employee = null;


                if(((EmployeeService.EmployeeType)Convert.ToInt32(DropDownList1.SelectedValue))
                    == EmployeeService.EmployeeType.FullTimeEmployee)
                {
                    employee = new EmployeeService.FullTimeEmployee
                    {
                        Type = EmployeeService.EmployeeType.FullTimeEmployee,
                        MonthlySalary = Convert.ToInt32(txtMonthlySalary.Text)
                    };
                }
                else if (((EmployeeService.EmployeeType)Convert.ToInt32(DropDownList1.SelectedValue))
                    == EmployeeService.EmployeeType.PartTimeEmployee)
                {
                    employee = new EmployeeService.PartTimeEmployee
                    {
                        Type = EmployeeService.EmployeeType.PartTimeEmployee,
                        HourlyPay = Convert.ToInt32(txtHourlyPay.Text),
                        HoursWorked = Convert.ToInt32(txtHoursWorked.Text)
                    };
                }
                employee.ID = Convert.ToInt32(txtId.Text);
                employee.Name = txtName.Text;
                employee.Gender = txtGender.Text;
                employee.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);

 
                client.SaveEmployee(employee);

                lblMessage.Text = "Employee saved";
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedValue == "-1")
            {
                trMonthlySalary.Visible = false;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else if (DropDownList1.SelectedValue == "1")
            {
                trMonthlySalary.Visible = true;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                trMonthlySalary.Visible = false;
                trHourlyPay.Visible = true;
                trHoursWorked.Visible = true;
            }


        }
    }
}