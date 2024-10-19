## Design Concept - The equivalent of a design notebook

#### 1. Introduction

Immersive and Innovative Teaching Technologies will enable an AI agent with sufficient subject knowledge to replace the teacher by linking speech-to-text and text-to-speech APIs.

In designing the Immersive and Innovative Instructional Technology system, we evaluated various technology options in detail and selected technology components that were appropriate for the needs of this project. The following documents our selection process, evaluation of alternatives, and rationale for our final decision. This will help the team understand why these technologies were selected and inform future technology decisions.

#### 2. Project background

The Internet-based teaching mode still has problems, such as the need for rich teacher resources, the difficulty of interaction between students and teachers, and the poor immersion of student learning.

With the rapid development of artificial intelligence technology, AI technology provides a new educational development model.

Immersive and Innovative Teaching Technologies will enable an AI agent with sufficient subject knowledge to replace the teacher. They are achieving a virtual or augmented space where multiple personified AI agents can talk to the user.

#### 3. **Reasons for technology selection**

##### 3.1 **Virtual Reality (VR) Engine**

- **Unity VR Environment Introduction**.
  - Unity is a widely used game engine that supports virtual reality (VR) development.
  - It provides rich tools (e.g. scene modelling, animation editing) and resources (e.g. direct import of 3D models) and supports a wide range of VR hardware.
  - Programming is done using C# scripts.

- **Unity detailed implementation**.
  - **Specific VR Adaptation**: Install the appropriate XR plugin for the target device (e.g. Oculus, HTC Vive).
  - **Code**: Use C# scripts to control VR controllers and interact with objects in the scene.
  - **3D Models**: Full models, including animation files, can be imported directly from external sources.

- **Summary of the overall VR scene editing process**.
  1. Create a new project using Unity Hub.
  2. Install and configure plugins: Install the appropriate VR plugins and SDKs.
  3. Create and configure the VR scene.
  4. Test with a VR device.

- **Other Alternative Engines**: **Unreal Engine**: The same.
  - **Unreal Engine**: It also has VR scene editing capabilities, similar to Unity.
  - **Godot Game Engine**: A lightweight engine that also supports VR development, requires the addition of Godot's VR plugin (e.g. Godot OpenXR) to support VR hardware.

##### 3.2 **Speech-to-Text Technology Route**

**Speech-to-Text Interface**: use OpenXR to convert speech to text.

- **API**: Uses OpenAI's transcription and translation service based on the open-source large-scale model Whisper.
  - Transcribe audio to any language.
  - Translate and transcribe audio to English.
  - The file upload limit is 25 MB, and the following input file types are supported: mp3, mp4, mpeg, mpga, m4a, wav, and webm.
  - It is one of the few open-source models OpenAI provides, with high acceptance and complete documentation.
  - Supports local deployment.

##### 3.3 **Text-to-Speech Technology Route**

- **Speech-to-Text Interfaces API** : 
  - **Conversational optimisation**: ChatTTS focuses on conversational tasks to ensure the naturalness and fluency of synthesised speech.
  - **Fine-grained control**: supports precise manipulation of sound elements such as laughter, pauses, and inserted words.
  - **Multi-speaker support**: can simulate speakers of different genders and styles to increase the diversity of speech.
  - **Efficient Interface**: provides a simple and easy-to-use Python API for quick integration into existing projects.
  - **Multi-language support**: supports Chinese and English, is suitable for multi-language environments and meets the needs of users of different languages.
  - **Large-scale data training**: It uses about 100,000 hours of Chinese and English data for training, resulting in high-quality speech synthesis and natural sound.
  - **Open Source**: The open source project encourages further research and innovation, and provides pre-trained models.
  - **Ease of use**: Input text information to generate corresponding speech files, which is convenient for users who need speech synthesis.
  - **Conversation task compatibility**: Suitable for handling conversation tasks that are usually assigned to large language models (LLMs), providing a more natural and smooth interactive experience.
  - **Control and Security**: Committed to improving the controllability of the model, adding watermarks, and integrating it with LLMs to ensure the security and reliability of the model.

##### 3.4 **AI Technology Path Confirmation**

**Introduction to Spring AI framework

- As an API management tool, Spring AI is compatible with a wide range of local and remote AI models and has the following advantages:
  - Support for different types of language models and compatibility with some speech-to-text models (such as OpenAI Whisper).
  - SpringBoot provides complete authentication and authorisation functionality and can be used as a full backend for the system.
  - It can be integrated with Lombok and Maven dependency management and other development tools to facilitate development.
  - Support for modular development, MVC, AOP, and other concepts.
  - Provide a one-click packaging function that is convenient for project deployment.
  - Provide detailed official documentation, and the prosperous community also has many teaching videos and blogs.

- **Possible defects**.
  - Java itself executes slowly and may have some delays.

**Language Models**.

- **Remote LLM - OpenAI**.
  - As an industry leader, OpenAI offers several powerful and convenient interfaces with the following advantages.
    - The GPT-3 Turbo/GPT-4 data provided has an extensive training volume and excellent performance.
    - The official management platform is complete and easy to use.
    - Detailed documentation for easy understanding and learning.
    - Other AI services (e.g. speech-to-text) are also provided.

  - **Weaknesses**.
    - The official API requires a fee, and there may be restrictions on its use.
    - Function implementation depends on stable network conditions.

- **Local - Ollama/Open WebUI**.
  - **Ollama** is an open-source LLM service tool that integrates model download, management, and other features, allowing big model developers, researchers, and enthusiasts to quickly experiment with, manage, and deploy the latest big language models in a local environment.
  - **Open WebUI** is a packaged local extensive prediction interface that provides a ChatGPT-like web interface and also delivers OpenAI-style API results to help developers work with big language models more efficiently.

  - **Benefits**.
    - Local LLMs offer some unique advantages over remote APIs - data privacy, security, offline availability, and reduced dependence on external services.
    - Meta's open-source model, LLaMa3, is close to GPT-4 in all metrics and performs well.

  - **Disadvantages**.
    - Local deployment has high hardware requirements, and continuous operation needs to provide sufficient video memory, RAM, and cooling.
    - Local deployment will make the module structure more complicated.
    - The API provided by Open WebUI is not as perfect as the OpenAI platform, which may require more effort to implement certain functions.

#### 4. **Conclusion**

Considering that this project's security and latency requirements are not particularly strict, to reduce the development difficulty and module complexity, we recommend using Spring AI + OpenAI's API + Whisper + Chat TTS to complete the implementation of the AI module. The combination of these technologies can provide an efficient, flexible and high-quality solution that meets the main requirements of the project.