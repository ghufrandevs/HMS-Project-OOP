

namespace HMS_project
{
    internal class Program
    {
        static List<Patient> patients = new List<Patient>();
        static List<Doctor> doctors = new List<Doctor>();
        const double BASE_SALARY = 300;
        const double BONUS_PER_VISIT = 15;
        public static void ShowMenu()
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

            doctors.Add(new Doctor("Dr. Noor", 5, 0));
            doctors.Add(new Doctor("Dr. Salem", 3, 0));
            doctors.Add(new Doctor("Dr. Hana", 8, 0));


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

            Patient newPatient = new Patient(Name, Diagnosis, department, typeBlood);

            patients.Add(newPatient);

            Console.WriteLine("the Patient registered successfully! ");
            Console.WriteLine("Patient ID is " + newPatient.patientID);

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
                if (patients[i].patientName.ToLower() == input.Trim().ToLower() || patients[i].patientID.ToLower() == input.Trim().ToLower())
                {
                    return patients[i]; 
                }
            }
            return null;
        }
        public static void AdmittedPatient()
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
                Doctor foundDoctor = SearchDoctor(doctorInput);
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
                    admitResult.visitCount++;
                    admitResult.lastVisitDate = DateTime.Now;
                    admitResult.lastDischargeDate = DateTime.MinValue;
                    admitResult.assignedDoctors=foundDoctor.doctorNames  ;
                    foundDoctor.doctorAvailableSlots--;
                    foundDoctor.doctorVisitCount++;
                    if(admitResult.visitCount==1)
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
      
        public static Doctor SearchDoctor(string input)
        {
            for( int i = 0;i< doctors.Count;i++)
            {
                if (input.ToLower() == doctors[i].doctorNames.ToLower())
                {
                    return doctors[i];
                }
               
            }
            return null;

        }

        public static void AddDoctor()
        {
            Console.WriteLine("Enter the Doctor Name");
            string doctorName = (Console.ReadLine() ?? string.Empty).Trim();
            doctorName= doctorName.Replace("Dr ", "Dr. ");
            Doctor doctorInput= SearchDoctor(doctorName);
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
                doctors.Add(new Doctor(doctorName, slots, 0));
                Console.WriteLine("Doctor : " + doctorName + " registered successfully with " + slots + " available slots.");

            }
            else
            {
                Console.WriteLine("Doctor already exists.");
            }

        }
        public static void DischargePatient()
        {
            Console.WriteLine("Enter Patient ID or Name:");
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            Patient p = SearchPatient(input);
            if(p==null)
            {
                Console.WriteLine("Patient not found");
                return;
            }
            if (p.admitted == false)
            {
                Console.WriteLine("This patient is not currently admitted");
                return;
            }
            double totalCharge = 0;
            totalCharge += AskCharge("Was there a consultation fee? (yes/no)");
            totalCharge += AskCharge("Any medication charges? (yes/no)");

            p.billingAmount += totalCharge;

            //The doctor associated with the patient
            string doctorName = p.assignedDoctors;
            Doctor doctor = SearchDoctor(doctorName);
            if (doctor == null)
            {
                Console.WriteLine("Warning: assigned doctor not found in registry. Slots not updated.");
            }
            else
            {
                doctor.doctorAvailableSlots++;
                Console.WriteLine("Doctor name: " + doctor.doctorNames +
                                  " now has " + doctor.doctorAvailableSlots + " slot(s) available.");
            }
            p.dischargePatient();
            p.assignedDoctors = "";
            //Counting the days
            int days = (p.lastDischargeDate - p.lastVisitDate).Days;
            //Ensures that the minimum hospital stay is counted as 1 day. 
            //If the calculated stay is 0(same - day discharge), it is adjusted to 1 to avoid zero - day billing.
            if (days == 0) days = 1;

            p.daysInHospital += days;

            if (totalCharge > 0)
            {
                Console.WriteLine("Total charges added this visit: " + Math.Round(totalCharge, 2) + " OMR");
                Console.WriteLine("Total billing amount: " + Math.Round(p.billingAmount, 2) + " OMR");
            }
            else
            {
                Console.WriteLine("No charges recorded");
            }

            Console.WriteLine("Patient discharged successfully on " +
                p.lastDischargeDate.ToString("yyyy-MM-dd HH:mm"));

            Console.WriteLine("Days in this visit: " + days);
            Console.WriteLine("Total days in hospital: " + p.daysInHospital);
        }
        
        public static double AskCharge(string question)
        {
            while (true)
            {
                Console.WriteLine(question);
                string answer = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

                if (answer == "yes")
                {
                    Console.WriteLine("Enter amount:");

                    if (!double.TryParse(Console.ReadLine(), out double amount))
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                        continue;
                    }

                    if (amount <= 0)
                    {
                        Console.WriteLine("Amount must be greater than 0.");
                        continue;
                    }

                    return amount;
                }
                else if (answer == "no")
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
        }
        public static void ListAdmittedPatients()
        {
            Console.WriteLine("Filter by name (press Enter to skip):");
            string keyword = (Console.ReadLine() ?? "").ToLower();

            bool found = false;
            int count = 0;
            double maxBilling = 0;

            foreach (var p in patients)
            {
                if (p.admitted &&
                   (string.IsNullOrEmpty(keyword) || p.patientName.ToLower().Contains(keyword)))
                {
                    p.printPatient();
                    found = true;
                    count++;

                    if (p.billingAmount > maxBilling)
                    {
                        maxBilling = p.billingAmount;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("No admitted patients.");
            }
            else
            {
                Console.WriteLine("Total admitted: " + count);
                Console.WriteLine("Highest billing: " + maxBilling + " OMR");
            }
        }


        public static void TransferDoctor()
        {
            // NOTE: Only the first matching patient is transferred.
            // The loop stops after one transfer using 'break' to prevent moving all patients under the same doctor.
            Console.WriteLine("Enter Current Doctor Name:");
            string currentDoctorName = (Console.ReadLine() ?? string.Empty).Trim();

            Console.WriteLine("Enter New Doctor Name:");
            string newDoctorName = (Console.ReadLine() ?? string.Empty).Trim();

            // Normalize
            currentDoctorName = currentDoctorName.Replace("Dr ", "Dr. ");
            newDoctorName = newDoctorName.Replace("Dr ", "Dr. ");

            if (currentDoctorName.ToLower() == newDoctorName.ToLower())
            {
                Console.WriteLine("The doctor names must be different");
                return;
            }

            Doctor currentDoctor = SearchDoctor(currentDoctorName);
            Doctor newDoctor = SearchDoctor(newDoctorName);

            if (currentDoctor == null || newDoctor == null)
            {
                Console.WriteLine("One of the doctors not found");
                return;
            }

            bool found = false;

            foreach (var p in patients)
            {
                if (p.admitted &&
                    p.assignedDoctors != null &&
                    p.assignedDoctors.ToLower() == currentDoctorName.ToLower())
                {
                    if (newDoctor.doctorAvailableSlots <= 0)
                    {
                        Console.WriteLine("New doctor has no slots");
                        return;
                    }
                    //transfer doctor
                    p.assignedDoctors = newDoctor.doctorNames;

                    //update slots
                    newDoctor.doctorAvailableSlots--;
                    currentDoctor.doctorAvailableSlots++;

                    Console.WriteLine("Patient " + p.patientName + " has been transferred to " + newDoctor.doctorNames);
                    Console.WriteLine("Patient last admitted on " + p.lastVisitDate.ToString("yyyy-MM-dd"));

                    found = true;
                    break; 
                }
            }

            if (!found)
            {
                Console.WriteLine("No admitted patient found under this doctor");
            }
        }
        public static void SearchDepartment()
        {
            Console.WriteLine("Enter the Department:");
            string searchDep = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

            Console.WriteLine("Patients in department '" + searchDep.ToUpper() + "':");

            bool patientAvailable = false;

            foreach (var p in patients)
            {
                if (p.departments != null && p.departments.ToLower().Contains(searchDep))
                {
                    patientAvailable = true;
                    Console.WriteLine("Patient Name: " + p.patientName);
                    Console.WriteLine("Patient ID: " + p.patientID);

                    string displayDiagnosis;
                    if (p.diagnoses != null && p.diagnoses.Length > 15)
                    {
                        displayDiagnosis = p.diagnoses.Substring(0, 15) + "...";
                    }
                    else
                    {
                        displayDiagnosis = p.diagnoses;
                    }

                    Console.WriteLine("Diagnosis: " + displayDiagnosis);
                    Console.WriteLine("Blood Type: " + p.bloodType);

                    if (p.admitted)
                    {
                        Console.WriteLine("Admission status: Admitted");
                    }
                    else
                    {
                        Console.WriteLine("Admission status: Not Admitted");
                    }

                    Console.WriteLine("----------------------");
                }
            }

            if (!patientAvailable)
            {
                Console.WriteLine("No patients found in this department");
            }
        }
        public static void SystemWideTotalBilling()
        {
            double totalAmount = 0;
            double max = 0;
            double min = 0;
            bool found = false;

            foreach (var p in patients)
            {
                if (p.billingAmount > 0)
                {
                    totalAmount += p.billingAmount;

                    if (!found)
                    {
                        max = p.billingAmount;
                        min = p.billingAmount;
                        found = true;
                    }
                    else
                    {
                        max = Math.Max(max, p.billingAmount);
                        min = Math.Min(min, p.billingAmount);
                    }
                }
            }

            Console.WriteLine("Total amount: " + Math.Round(totalAmount, 2) + " OMR");

            if (found)
            {
                Console.WriteLine("Highest individual billing: " + Math.Round(max, 2) + " OMR");
                Console.WriteLine("Lowest individual billing: " + Math.Round(min, 2) + " OMR");
            }
        }

        public static void IndividualPatientBillingReport()
        {
            Console.WriteLine("Enter Patient ID:");
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            Patient p = SearchPatient(input);

            if (p == null)
            {
                Console.WriteLine("Patient not found");
                return;
            }

            if (p.billingAmount == 0)
            {
                Console.WriteLine("No billing records");
                return;
            }

            Console.WriteLine("Total: " + Math.Round(p.billingAmount, 2) + " OMR");
            Console.WriteLine("Last Visit Date: " + p.lastVisitDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("Total Days: " + p.daysInHospital);

            int discount = Randomdiscount();
            double discounted = p.billingAmount - (p.billingAmount * discount / 100);

            Console.WriteLine("Discount applied: " + discount + "%");
            Console.WriteLine("After discount: " + Math.Round(discounted, 2) + " OMR");
        }

        static Random rand = new Random();
        public static int Randomdiscount()
        {
            return rand.Next(5, 21);
        }

        public static void ViewMostVisit()
        {
            Console.WriteLine("Most visited patients:");

            int maxVisit = 0;
            foreach (var p in patients)
            {
                if (p.visitCount > maxVisit)
                    maxVisit = p.visitCount;
            }
            for (int count = maxVisit; count >= 0; count--)
            {
                foreach (var p in patients)
                {
                    if (p.visitCount == count)
                    {
                        Console.WriteLine("Patient ID: " + p.patientID + " | Name: " + p.patientName + " | Department: " + p.departments +
                         " | Diagnosis: " + p.diagnoses + " | Visit Count: " + p.visitCount);
                    }
                }
            }
        }

        public static void DoctorSalaryReport()
        {
            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors registered in this system!");
                return;
            }

            double maxSalary = 0;
            Doctor topDoctor = null;

            foreach (var d in doctors)
            {
                double doctorSalary = BASE_SALARY + (d.doctorVisitCount * BONUS_PER_VISIT);
                doctorSalary = Math.Round(doctorSalary, 2);

                Console.WriteLine("Dr. " + d.doctorNames + " || Visits: " + d.doctorVisitCount +
                 " || Available Slots: " + d.doctorAvailableSlots +
                 " || Salary: " + doctorSalary);

                if (topDoctor == null || doctorSalary > maxSalary)
                {
                    maxSalary = doctorSalary;
                    topDoctor = d;
                }
            }
            Console.WriteLine("----------------------");

            if (topDoctor != null)
            {
                Console.WriteLine("Highest earning doctor: " +
                                  topDoctor.doctorNames + " — " +
                                  Math.Round(maxSalary, 2) + " OMR");
            }
        }
        static void Main(string[] args)
        {         
            SeedData();
            int option = 0;
            bool exit = false;
            while(!exit)
            {

                ShowMenu();
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
                        AdmittedPatient();
                        break;
                        case 3:
                        DischargePatient();
                        break;

                        case 4:
                        Console.WriteLine("Enter the Patient ID or Name :");
                        string input=(Console.ReadLine()?? string.Empty).Trim();
                        Patient foundPatient = SearchPatient(input);
                        if(foundPatient==null)
                        {
                            Console.WriteLine("Patient not found ");
                        }
                        else
                        {
                            foundPatient.printPatient();
                        }

                            break;

                        case 5:
                        ListAdmittedPatients();
                        break;
                        case 6:
                        TransferDoctor();
                        break;
                        case 7:ViewMostVisit();
                        break;
                        case 8:
                        SearchDepartment();
                        break;
                        case 9:
                        Console.WriteLine("Please choose option:");
                        Console.WriteLine("1. System-wide total");
                        Console.WriteLine("2. Individual patient");

                        if (!int.TryParse(Console.ReadLine(), out int options))
                        {
                            Console.WriteLine("Invalid input");
                            break;
                        }

                        switch (options)
                        {
                            case 1:
                                SystemWideTotalBilling();
                                break;

                            case 2:
                                IndividualPatientBillingReport();
                                break;

                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                        case 10:
                        AddDoctor();
                        break;
                        case 11:
                        DoctorSalaryReport();
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
        public static int patientCount = 0;
        
        public Patient(string patientName, string diagnoses, string departments, string bloodType)
        {
            this.patientName = patientName;
            this.diagnoses = diagnoses;
            this.departments = departments;
            this.bloodType = bloodType;
            //defult value
            admitted = false;
            assignedDoctors = "";
            visitCount = 0;
            billingAmount = 0;
            lastVisitDate = DateTime.MinValue;
            lastDischargeDate = DateTime.MinValue;
            daysInHospital = 0;
            patientCount++;
            patientID = "P" + patientCount.ToString("D3");
            
        }
        public void printPatient()
        {
            Console.WriteLine("the patient details : ");
            Console.WriteLine("Patient ID:  " + patientID.ToUpper());
            Console.WriteLine("patient Name:  " + patientName);
            Console.WriteLine("Diagnosis: " + diagnoses + " (" + diagnoses.Length + " characters)");
            Console.WriteLine("department: " + departments);
            Console.WriteLine(" blood Type: " + bloodType.ToUpper());
            Console.WriteLine(" admission status: " + admitted);
            Console.WriteLine(" visit count: " + visitCount);
            Console.WriteLine(" total billing amount: " + Convert.ToString(Math.Round(billingAmount, 2)) + " OMR");
            if (admitted)
                Console.WriteLine("Assigned Doctor: " + assignedDoctors);
            else
                Console.WriteLine("Not currently admitted");

            Console.WriteLine("Last Visit Date: " +
                (lastVisitDate == DateTime.MinValue ? "N/A" : lastVisitDate.ToString()));

            Console.WriteLine("Last Discharge Date: " +
                (lastDischargeDate == DateTime.MinValue ? "N/A" : lastDischargeDate.ToString()));

            Console.WriteLine("Total Days in Hospital: " + daysInHospital);
        }
        public void dischargePatient()
        {
            admitted = false;
            lastDischargeDate = DateTime.Now;
        }
    }
        class Doctor
        {
            public string doctorNames;
            public int doctorAvailableSlots;
            public int doctorVisitCount;
        
            public Doctor(string doctorNames, int doctorAvailableSlots, int doctorVisitCount)
            {
                this.doctorNames = doctorNames;
                this.doctorAvailableSlots = doctorAvailableSlots;
                this.doctorVisitCount = doctorVisitCount;
            }

        }
    }

    
    
