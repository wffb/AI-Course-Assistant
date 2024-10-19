# Sprint Backlog

## Sprint 2 - Goal

Complete foundational tasks related to student interaction with AI, teacher account creation, and basic system functionality.

### Timeframe

Week 6 - week 8 (26/8/2024 - 16/9/2024)

### Workload

25

### Additional Notes

- The team will focus on user stories U01, U02, U09, U10, U11, and U12.
- This sprint focuses on setting up the essential user authentication (for both students and teachers), basic student interaction with AI, and foundational functionalities.
- Speech-to-Text and Text-to-Speech functions will be developed in parallel to the user account management.

### Sprint 2 Backlog

| ID  | Sprint User Story                                                                                                                                                                                   | Subtask ID | Subtask                                                                                                                 | Corresponding AC | Assigned to                      | Priority | Estimation (story points) | Dependencies |
| --- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- | ----------------------------------------------------------------------------------------------------------------------- | ---------------- | -------------------------------- | -------- | ------------------------- | ------------ |
| U09 | As a Student, I want to be able to create an account, so that I can access my subject resources.                                                                                                    | U09_T1     | Design and implement the student registration and login UI in Unity                                                     | U09_AC1-2        | Shanqing Huang                   | High     | 1                         | -            |
|     |                                                                                                                                                                                                     | U09_T2     | Implement backend logic for student account creation.                                                                   | U9_AC3           | Lisong Xiao                      | Medium   | 1                         | U09_T1       |
| U10 | As a Student, I want to be able to login to an account, so that I can converse with AI.                                                                                                             | U10_T1     | Develop the login functionality for students.                                                                           | U10_AC1          | Lisong Xiao                      | High     | 1                         | U09_T2       |
| U11 | As a Teacher, I want to be able to create an admin account, so that I can create different agents and perform admin tasks.                                                                          | U11_T1     | Design and implement the teacher registration form interface.                                                           | U11_AC1-2        | Lisong Xiao                      | High     | 1                         | -            |
|     |                                                                                                                                                                                                     | U11_T2     | Implement backend logic for teacher account creation.                                                                   | U11_AC3          | Lisong Xiao                      | Medium   | 1                         | U11_T1       |
| U12 | As a Teacher, I want to login to application as an admin, so that I can view an admin dashboard with access to things such as all available student logs, tutors, and active learning environments. | U12_T1     | Develop the login functionality for teacher.                                                                            | U12_AC1-3        | Lisong Xiao                      | High     | 1                         | U11_T2       |
| U01 | As a Student, I want to be able to directly talk to AI about my study, so that I can have a more efficient way of gaining information.                                                              | U01_T1     | Obtain and securely store the OpenAI API keys.                                                                          | U01_AC1          | Zhuyun Lu                        | High     | 2                         | -            |
|     |                                                                                                                                                                                                     | U01_T2     | Integrate OpenAI Assistant API by creating a C# script in Unity that handles HTTP requests to the OpenAI Assistant API. | U01_AC2          | Bo Huang, Lisong Xiao, Zhuyun Lu | High     | 6                         | U01_T1       |
|     |                                                                                                                                                                                                     | U01_T3     | Implement speech recognition to convert user voice to text using Unity-compatible libraries or services.                | U01_AC3          | Zhuyun Lu                        | High     | 3                         | U01_T2       |
| U02 | As a Student, I want to be able to listen to the feedback from AI immediately, so that I can get the answer quick and easy.                                                                         | U02_T1     | Create a basic scene with a UI and a 3D model of a person.                                                              | U02_AC1          | Ziqi Wang, Haoran Wang           | High     | 2                         | -            |
|     |                                                                                                                                                                                                     | U02_T2     | Develop a UI in Unity that allows users to input questions and receive responses.                                       | U02_AC1          | Shanqing Huang                   | High     | 3                         | U02_T1       |
|     |                                                                                                                                                                                                     | U02_T3     | Implement text-to-speech functionality to vocalize the text responses from the AI.                                      | U02_AC2-3        | Bo Huang                         | High     | 3                         | U02_T2       |

## Estimation Process

### Method Used

Our team uses **Planning Poker**, a consensus-based agile estimation technique, to estimate story points for tasks.

### Procedure

- **Gathering Requirements**: Prior to estimation, the Product Owner ensures all user stories are clearly defined and understood.
- **Discussion**: Each user story is discussed thoroughly to ensure a comprehensive understanding among all team members.
- **Execution**:
  - Team members propose story points based on their understanding and expertise.
  - A consensus is sought through discussion. If discrepancies exist, further clarification is provided, and re-estimation may occur if necessary.
- **Recording Estimates**: All agreed-upon estimates are recorded in GitHub markdown files to ensure transparency and traceability.
- **Adjustments**:
  - **Initiation of Changes**: Team members can propose adjustments to story points based on new insights or unexpected complexities encountered during development.
  - **Approval Process**: Adjustments must be reviewed and approved by the Product Owner to ensure they are justified and reasonable.
  - **Documentation**: Any changes to story points are manually updated in a GitHub markdown file, with a detailed record of the rationale behind the adjustment.

### Tools and Resources

- **GitHub Markdown**: Utilized to document and track all task assignments and any changes to story points.

## Burndown Chart

![Burndown Chart](/docs/Sprint2/Sprint2BurndownChart.png)
