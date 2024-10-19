```mermaid
graph TD
    subgraph "User Interaction Layer"
        A[VR Headset]
        B[User Interface]
    end
    
    subgraph "Core AI System"
        C[AI Dialogue Manager]
        D[Knowledge Base]
        E[Conversation History]
    end
    
    subgraph "Speech Processing"
        F[Speech Recognition]
        G[Text-to-Speech]
    end
    
    subgraph "3D Visualization"
        H[3D Rendering Engine]
        I[3D Model Library]
        J[Lip Sync Module]
    end
    
    subgraph "Management Layer"
        K[Teacher Management Interface]
        L[AI Agent Configuration]
        M[Student Management]
    end
    
    subgraph "Support Systems"
        N[Authentication System]
        O[External Systems Integration]
    end
    
    A ---> B
    B ---> F
    B ---> H
    F ---> C
    C ---> G
    G ---> B
    C <--> D
    C <--> E
    H <--> I
    H <--> J
    K ---> L
    K ---> M
    N ---> B
    N ---> K
    O ---> C
