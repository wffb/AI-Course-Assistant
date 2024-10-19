# Backlog Change Log

## Change Overview

- **Previous Focus**: Equally balanced between developing user login functionalities and AI interaction features.
- **New Focus**: Primarily on enhancing AI functionalities, with a specific emphasis on creating and managing customizable AI agents for educational purposes.

## Reasons for Change

- **Client Request**: The client has directed a shift in focus towards AI functionalities to leverage advanced interactive and educational technologies. This change aims to enhance user engagement and educational outcomes through more personalized and dynamic AI interactions.
- **Deprioritization of User Login Features**: The client indicated that user login functionalities are not a priority at this stage, suggesting a need to streamline the project to focus resources more effectively on core AI features.

## Detailed Changes

- **Added Features**:

  - Development of a teacher interface for creating and configuring customizable AI agents.
  - Implementation of a student interface in Unity for selecting AI agents, with scenarios changing based on the selected agent.
  - Integration of item interaction functionalities in Unity, where the system records and synchronizes information about items students interact with, to the assistance AI.

- **Removed or Modified Features**:
  - Scaling down the development efforts on comprehensive user login and session management features.
  - Adjustments to the backend infrastructure to support dynamic AI configurations rather than user management.

## Changes in Sprint Backlog

### Added New Tasks
| ID | Sprint User Story | Subtask ID | Subtask | Corresponding AC | Assigned to | Priority | Estimation (story points) | Dependencies |
| --- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- | ----------------------------------------------------------------------- | ---------------- | ----------- | -------- | ------------------------- | ------------ |
| U10 | As a Student, I want to be able to login to an account, so that I can converse with AI. | U10_T2 | Develop a selection interface where students can choose their AI agent. | U10_AC2 | Haoran Wang | High | 1 | U10_T1 |
| | | U10_T3 | Integrate the chosen AI agent into the Unity environment. | U10_AC3 | Haoran Wang | High | 1 | U10_T2 |
| U15 | As a Student, I want to be able to interact with items in the virtual environment, so that I can have a more specific and clear understanding of the items I am learning. | U15_T3 | Develop a system to track and store information on items students interact with. | U15_AC3 | Shanqing Huang | High | 1 | |
| | | U15_T4 | Synchronize item interaction data with the AI agent. | U15_AC4 | Shanqing Huang | High | 1 | |
