## Backlog Change Log

### Changes

- **Previous Requirement**: Interaction with AI implemented through a frontend and backend framework.
- **New Requirement**: Interaction with AI is to be directly implemented using C# in Unity, calling the OpenAI Assistant API for generating responses.

### Reason for Change

- This change was initiated at the request of the client to simplify the deployment process by reducing the number of system components and to leverage Unity's native capabilities for enhanced real-time interaction within the virtual environment.

### Changes in Product Backlog

| ID  | Task                                                                                                            | User Story                                                                                                                             | Acceptance Criteria                                                                                                                                                                                                                                                                                                                                                                                                                | Priority | Estimation (story points) | Dependency |
| --- | --------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------- | ------------------------- | ---------- |
| U01 | Implement functionality to send voice data to the OpenAI Assistant API and receive responses directly in Unity. | As a Student, I want to be able to directly talk to AI about my study, so that I can have a more efficient way of gaining information. | AC1: API keys are stored securely and can be accessed by the application without exposing them in the codebase.<br>AC2: The module can send user input to the OpenAI Assistant API and receive a response without errors. The module should handle errors gracefully and log them appropriately.<br>AC3: Voice inputs are accurately converted to text and used as prompts for the OpenAI Assistant API.                           | High     | 11                        | None       |
| U02 | Modify the AI feedback mechanism to handle real-time text-to-speech processing using the API's response.        | As a Student, I want to be able to listen to the feedback from AI immediately, so that I can get the answer quick and easy.            | AC1: Once project is open, there is a basic scene with a UI where users can input questions and receive responses. And a button can be pressed after the user inputs their question.<br>AC2: Users can input voice data, which is then converted to text and displayed on the UI before being sent to the API.<br>AC3: Text responses from the AI are converted to clear and audible speech, enhancing the interactive experience. | High     | 11                        | U01        |

### Changes in Sprint Backlog

**Sprint 2 Backlog**
| ID | Sprint User Story | Subtask ID | Subtask | Corresponding AC | Assigned to | Priority | Estimation (story points) | Dependencies |
| --- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- | ----------------------------------------------------------------------------------------------------------------------- | ---------------- | -------------------------------------- | -------- | ------------------------- | ------------ |
| U01 | As a Student, I want to be able to directly talk to AI about my study, so that I can have a more efficient way of gaining information. | U01_T1 | Obtain and securely store the OpenAI API keys. | U01_AC1 | Zhuyun Lu, Bo Huang, Lisong Xiao | High | 2 | - |
| | | U01_T2 | Integrate OpenAI Assistant API by creating a C# script in Unity that handles HTTP requests to the OpenAI Assistant API. | U01_AC2 | Zhuyun Lu, Bo Huang, Lisong Xiao | High | 6 | U01_T1 |
| | | U01_T3 | Implement speech recognition to convert user voice to text using Unity-compatible libraries or services. | U01_AC3 | | High | 3 | U01_T2 |
| U02 | As a Student, I want to be able to listen to the feedback from AI immediately, so that I can get the answer quick and easy. | U02_T1 | Create a basic scene with a UI and a 3D model of a person. | U02_AC1 | Shanqing Huang, Ziqi Wang, Haoran Wang | High | 2 | - |
| | | U02_T2 | Develop a UI in Unity that allows users to input questions and receive responses. | U02_AC1 | Shanqing Huang, Ziqi Wang, Haoran Wang | High | 3 | U02_T1 |
| | | U02_T3 | Implement text-to-speech functionality to vocalize the text responses from the AI. | U02_AC2-3 | | High | 6 | U02_T2 |

### Impact on Schedule

- **Training**: Additional time will be allocated for training the team on Unity and C# scripting for API integration.
- **Delays**: Initial delays as the team adjusts to new technologies and the revised implementation approach.
- **Sprint 2 Goal** At sprint 2, we focus on set up development environment and develop a 2D version of the software that hasl basic AI conversation features(including U01 and U02).

### Impact Analysis

- **Complexity Reduction**: Eliminates the need for maintaining a separate backend infrastructure, simplifying the overall architecture.
- **Dependency**: Increased dependency on the stability and availability of the OpenAI Assistant API.
- **Skill Gaps**: The team may require training or possibly new hires with expertise in Unity, C#, and API integrations.
- **Productivity**: Short-term productivity decrease as the team learns the new stack but potentially higher efficiency once acclimated.
