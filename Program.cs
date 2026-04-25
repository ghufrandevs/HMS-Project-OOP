using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace HMS_project
{
    internal class Program
    {
        static List<Patient> patients = new List<Patient>();
        static List<Doctor> Doctors = new List<Doctor>();
        public static void showMenu()
        {
            Console.WriteLine("Hello to Managing Health Care Clinic");
            Console.WriteLine("Please choice option : ");
            Console.WriteLine("1.Register New Patient");
            Console.WriteLine("2.Admit Patient ");
            Console.WriteLine("3.Discharge Patient");
            Console.WriteLine("4.Search Patient");
            Console.WriteLine("5.List All Admitted Patients");
            Console.WriteLine("6.Transfer Patient to Another Doctor");
            Console.WriteLine("7.View Most Visited Patients ");
            Console.WriteLine("8.Search Patients by Department ");
            Console.WriteLine("9.Billing Report");
            Console.WriteLine("10.Add Doctor");
            Console.WriteLine("11.Doctor Salary Report");
            Console.WriteLine("12.Exit");


        }
        public static void SeedData()
        {
            patients.Add(new Patient("Ali Hassan", "Flu", "General", "A+"));
            patients.Add(new Patient("Sara Ahmed", "Fracture", "Orthopedics", "O-"));
            patients.Add(new Patient("Omar Khalid", "Diabetes", "Cardiology", "B+"));

            Doctors.Add(new Doctor("Dr. Noor", 5, 0));
            Doctors.Add(new Doctor("Dr. Salem", 3, 0));
            Doctors.Add(new Doctor("Dr. Hana", 8, 0));


        }
        public static void RegisterPatient()
        {
            Console.WriteLine("Enter the patient name : ");
            string Name = (Console.ReadLine() ?? string.Empty).Trim();

            while (string.IsNullOrWhiteSpace(Name))
            {
                Console.WriteLine("Name cannot be empty. Please re-enter:");
                Name = (Console.ReadLine() ?? string.Empty).Trim();
            }
            Console.WriteLine("Enter the Diagnosis :");
            string Diagnosis = (Console.ReadLine() ?? string.Empty).Trim();
            while (string.IsNullOrWhiteSpace(Diagnosis))
            {
                Console.WriteLine("Diagnosis cannot be empty. Please re-enter:");
                Diagnosis = (Console.ReadLine() ?? string.Empty).Trim();
            }
            Console.WriteLine("Enter Department");
            string department = (Console.ReadLine() ?? string.Empty).Trim();

            while (string.IsNullOrWhiteSpace(department))
            {
                Console.WriteLine("Department cannot be empty:");
                department = (Console.ReadLine() ?? string.Empty).Trim();
            }
            Console.WriteLine("Enter Blood type : ");
            string typeBlood = (Console.ReadLine() ?? string.Empty).Trim();

            patients.Add(new Patient(Name, Diagnosis, department, typeBlood));


        }

        public static bool Exit()
        {
            Console.WriteLine("Are you sure you want to exit the system? (yes/no)");
            string inputs = (Console.ReadLine() ?? string.Empty).ToLower();
            if (inputs == "no")
            {
                return false;
            }
            else if (inputs == "yes")
            {
                Console.WriteLine("Thank you for using the system --Exit--");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid option");
                return false;
            }
            }
        
        public static Patient SearchPatient(string input)
        {
           for(int i = 0; i < patients.Count; i++)
            {
                if (patients[i].patientName.ToLower() == input.ToLower() || patients[i].patientID.ToLower() == input.ToLower())
                {
                    return patients[i]; 
                }
            }
            return null;
        }
        public static void admittedPatient()
        {
            Console.WriteLine("Enter the patient ID or Name: " );
            string input= (Console.ReadLine() ?? string.Empty).Trim().ToLower();
            Patient admitResult = SearchPatient(input);
            if(admitResult==null)
            {
                Console.WriteLine("patient not found ");  
            }
            else
            if(admitResult.admitted == true) 
            {            
               Console.WriteLine("Patient is already admitted under " + admitResult.assignedDoctors);
                
            }
            else
            {
                Console.WriteLine("Enter Doctor Name : ");
                string doctorInput = (Console.ReadLine() ?? string.Empty).Trim();
                doctorInput=doctorInput.Replace("Dr ", "Dr. ");
                Doctor foundDoctor = searchDoctor(doctorInput);
                if (foundDoctor == null)
                {
                    Console.WriteLine("Doctor not found in the system. Please register the doctor first.");
                    return;
                }
                if(foundDoctor.doctorAvailableSlots<=0)
                {
                    Console.WriteLine("doctor name: " + foundDoctor.doctorNames + " has no available slots at this time.");
                    return;
                }
                else
                {                
                    admitResult.admitted = true;
                    admitResult.lastVisitDate = DateTime.Now;
                    admitResult.lastDischargeDate = DateTime.MinValue;
                    admitResult.assignedDoctors=foundDoctor.doctorNames  ;
                    foundDoctor.doctorNames = admitResult.assignedDoctors;
                    foundDoctor.doctorAvailableSlots--;
                    foundDoctor.doctorVisitCount++;
                    if(admitResult.visitCount==0)
                    {
                        Console.WriteLine("Patient admitted for the first time and assigned with "
                              + admitResult.assignedDoctors+ " on "
                              + admitResult.lastVisitDate.ToString("yyyy-MM-dd HH:mm"));
                    }
                    else
                    {
                        Console.WriteLine("Patient admitted successfully and assigned to : " + admitResult.assignedDoctors+ " on " + admitResult.lastVisitDate.ToString("yyyy-MM-dd HH:mm"));

                        Console.WriteLine("This patient has been admitted "
                               + admitResult.visitCount + " times");
                    }
                    Console.WriteLine("Doctor name: " + foundDoctor.doctorNames + " now has " + foundDoctor.doctorAvailableSlots+ " slot(s) remaining.");

                }
            }
            }         
      
        public static Doctor searchDoctor(string input)
        {
            for( int i = 0;i< Doctors.Count;i++)
            {
                if (input.ToLower() == Doctors[i].doctorNames.ToLower())
                {
                    return Doctors[i];
                }
               
            }
            return null;

        }

        public static void addDoctor()
        {
            Console.WriteLine("Enter the Doctor Name");
            string doctorName = (Console.ReadLine() ?? string.Empty).Trim();
            doctorName= doctorName.Replace("Dr ", "Dr. ");
            Doctor doctorInput= searchDoctor(doctorName);
            if(doctorInput==null)
            {
                Console.WriteLine("Enter available slots :");
                if (!int.TryParse(Console.ReadLine(), out int slots) || slots < 1)
                {
                    Console.WriteLine("Invalid slot count. Doctor not registered ");
                    return;
                }
                // Validate slot count upper limit to prevent unrealistic values
                if (slots > 50)
                {
                    Console.WriteLine("Slot count too large.");
                    return;
                }
                Doctors.Add(new Doctor(doctorName, slots, 0));
                Console.WriteLine("Doctor : " + doctorName + " registered successfully with " + slots + " available slots.");

            }
            else
            {
                Console.WriteLine("Doctor already exists.");
            }

        }


        static void Main(string[] args)
        {         
            SeedData();
            int option = 0;
            bool exit = false;
            while(!exit)
            {

                showMenu();
                try
                {
                    option = int.Parse(Console.ReadLine());

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 12");

                }
                switch (option)
                {
                    case 1:
                        RegisterPatient();

                        break;

                        case 2:
                        admittedPatient();
                        break;
                        case 3:
                        break;

                    case 10:
                        addDoctor();
                        break;
                        case 12:
                        exit = Exit();
                        break;
                }
                if(!exit)
                {
                    Console.ReadLine();
                    Console.Clear();
                }

            }


        }
    }
    class Patient
    {
        public string patientName;
        public string patientID;
        public string diagnoses;
        public bool admitted;
        public string assignedDoctors;
        public string departments;
        public int visitCount;
        public double billingAmount;
        public DateTime lastVisitDate;
        public DateTime lastDischargeDate;
        public int daysInHospital;
        public string bloodType;
        public static int patientCount=0;
        public Patient(string patientName, string diagnoses, string departments, string bloodType)
        {
            this.patientName = patientName;
            this.diagnoses = diagnoses;
            this.departments = departments;
            this.bloodType= bloodType;
            //defult value
            admitted= false;
            assignedDoctors = "";
            visitCount = 0;
            billingAmount = 0;
            lastVisitDate = DateTime.MinValue;
            lastDischargeDate = DateTime.MinValue;
            daysInHospital = 0;
            patientID = ("P" + patientCount);
            patientCount++;
            Console.WriteLine("the Patient registered successfully! ");
            Console.WriteLine("Patient ID is " + patientID);





        }
    }

    class Doctor
    {
        public string doctorNames;
        public int doctorAvailableSlots;
        public int doctorVisitCount;

        public Doctor (string doctorNames, int doctorAvailableSlots, int doctorVisitCount)
        {
            this.doctorNames = doctorNames;
            this.doctorAvailableSlots = doctorAvailableSlots;
            this.doctorVisitCount = doctorVisitCount;
        }
    }
}