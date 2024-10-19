## Sprint 3 - Goal

Focus on advanced features, including AI agent management by teachers, conversation storage, and additional student interaction methods.

### Timeframe

Week 9 - week 11 (16/09/2024 - 14/10/2024)

### Workload

30

### Additional Notes

- The team will focus on user stories U03, U04, U05, U06, U07, U08, and U13.
- This sprint is to enhance AI functionalities and expand system capabilities for teachers and advanced student interactions.
- U6, U7, U13 will be developed in parallel to U3, U4, and U5.

### Sprint 3 Backlog

| ID  | Sprint User Story                                                                                                                                                            | Subtask ID | Subtask                                                                                             | Corresponding AC | Assigned to    | Priority | Estimation (story points) | Dependencies |
| --- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- | --------------------------------------------------------------------------------------------------- | ---------------- | -------------- | -------- | ------------------------- | ------------ |
| U06 | As a Teacher, I want to be able to create AI agents, so that I can create multiple AI agents for my students.                                                                | U06_T1     | Develop a management interface for AI agents                                                        | U06_AC1          | Zhuyun Lu      | High     | 1                         | -            |
|     |                                                                                                                                                                              | U06_T2     | Implement AI agent management backend Framework                                                     | U06_AC2          | Lisong Xiao    | High     | 2                         | U06_T1       |
| U07 | As a Teacher, I want to upload subject-related documents and prerequisite knowledge to AI agent, so that I can ensure the direction and range of the conversation.           | U07_T1     | Create a file upload and processing module                                                          | U07_AC1          | Zhuyun Lu      | Medium   | 2                         | U12_T1       |
|     |                                                                                                                                                                              | U07_T2     | Enhance AI understanding with uploaded content                                                      | U07_AC2          | Bo Huang       | Low      | 5                         | U07_T1       |
| U03 | As a Student, I want to be able to type to the AI, so that I can have multiple ways of conversing to AI if needed.                                                           | U03_T1     | Develop text-based communication features within the Unity interface                                | U03_AC1          | -              | Medium   | 2                         | U01          |
| U04 | As a Student, I want to be able to mute the AI but still observe the answer from AI in text, so that I can have a more convenient experience with additional options.        | U04_T1     | Implement features for muting AI and display text responses                                         | U04_AC1          | -              | Medium   | 2                         | U01          |
| U05 | As a Student, I want to stop the dialogue with AI, so that I can have seamless experience, stopping conversation if it gets too long/irrelevant.                             | U05_T1     | Implement features for terminating conversations.                                                   | U05_AC1          | -              | Medium   | 2                         | U01, U02     |
| U08 | As a Student, I want to save the conversation history, so that I can safely access them later (and model can learn from it).                                                 | U08_T1     | Implement conversation logging systems.                                                             | U08_AC1          | Bo Huang       | High     | 2                         | U01, U10     |
| U13 | As a Teacher, I want to be able to add students to subjects, so that students can access AI agents of the subjects.                                                          | U13_T1     | Develop the user interface for managing student accounts and courses                                | U13_AC1          | Zhuyun Lu      | High     | 1                         | U01, U10     |
|     |                                                                                                                                                                              | U13_T2     | Implement backend support for student and course management in Spring AI Framework                  | U13_AC2          | Lisong Xiao    | High     | 2                         | \*U08        |
| U14 | As a Student, I want to talk to the AI agent in a personified way using a 3D model of a person, so that I can talk to the AI agent as naturally as talking to a human being. | U14_T1     | Find the model of 3D character with animations of common gestures and lip-syncing to spoken output. | U14_AC1-2        | Ziqi Wang      | High     | 1                         |              |
|     |                                                                                                                                                                              | U14_T2     | Write a C# script in Unity to handle character model's responses.                                   | U14_AC3          | Shanqing Huang | High     | 1                         |              |
|     |                                                                                                                                                                              | U14_T3     | Find the model of 3d environment, import and setup in the Unity                                     | U14_AC3          | Ziqi Wang      | High     | 1                         |              |
|     |                                                                                                                                                                              | U14_T4     | Implement camera movement in Unity                                                                  | U14_AC3          | Shanqing Huang | High     | 1                         |              |
| U15 | As a Student, I want to be able to interact with items in the virtual environment, so that I can have a more specific and clear understanding of the items I am learning.    | U15_T1     | Script behaviors for interactive 3D models in Unity using C#.                                       | U15_AC1          | Ziqi Wang      | High     | 2                         |              |
|     |                                                                                                                                                                              | U15_T2     | Implement collision detection and response mechanics. responses.                                    | U15_AC2          | Ziqi Wang      | High     | 1                         |              |
|     |                                                                                                                                                                              | U15_T3     | Develop a system to track and store information on items students interact with.                    | U15_AC3          | Shanqing Huang | High     | 1                         |              |
|     |                                                                                                                                                                              | U15_T4     | Synchronize item interaction data with the AI agent.                                                | U15_AC4          | Shanqing Huang | High     | 1                         |              |

### Sprint 2 Remaining User Story

| ID  | Sprint User Story                                                                                                                                                                                   | Subtask ID | Subtask                                                                 | Corresponding AC | Assigned to | Priority | Estimation (story points) | Dependencies |
| --- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- | ----------------------------------------------------------------------- | ---------------- | ----------- | -------- | ------------------------- | ------------ |
| U09 | As a Student, I want to be able to create an account, so that I can access my subject resources.                                                                                                    | U09_T1     | Design and implement the student registration and login UI in Unity     | U09_AC1-2        | Haoran Wang | High     | 1                         | -            |
|     |                                                                                                                                                                                                     | U09_T2     | Implement backend logic for student account creation.                   | U9_AC3           | Lisong Xiao | Medium   | 1                         | U09_T1       |
| U10 | As a Student, I want to be able to login to an account, so that I can converse with AI.                                                                                                             | U10_T1     | Develop the login functionality for students.                           | U10_AC1          | Haoran Wang | High     | 2                         | U09_T2       |
|     |                                                                                                                                                                                                     | U10_T2     | Develop a selection interface where students can choose their AI agent. | U10_AC2          | Haoran Wang | High     | 1                         | U10_T1       |
|     |                                                                                                                                                                                                     | U10_T3     | Integrate the chosen AI agent into the Unity environment.               | U10_AC3          | Haoran Wang | High     | 1                         | U10_T2       |
| U11 | As a Teacher, I want to be able to create an admin account, so that I can create different agents and perform admin tasks.                                                                          | U11_T1     | Design and implement the teacher registration form interface.           | U11_AC1-2        | Bo Huang    | High     | 1                         | -            |
|     |                                                                                                                                                                                                     | U11_T2     | Implement backend logic for teacher account creation.                   | U11_AC3          | Lisong Xiao | Medium   | 1                         | U11_T1       |
| U12 | As a Teacher, I want to login to application as an admin, so that I can view an admin dashboard with access to things such as all available student logs, tutors, and active learning environments. | U12_T1     | Develop the login functionality for teacher.                            | U12_AC1-3        | Zhuyun Lu   | High     | 1                         | U11_T2       |

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

![Burndown Chart](/docs/Sprint3/Sprint3BurndownChart.png)
