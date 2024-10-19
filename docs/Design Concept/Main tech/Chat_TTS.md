# Text-to-speech technology route confirmation

## Text-to-speech (Speech to text) interface

### API

- Conversational optimisation: ChatTTS focuses on conversational tasks to ensure the naturalness and fluency of synthesised speech.
- Fine-grained control: supports precise manipulation of sound elements such as laughter, pauses, and inserted words.
- Multi-speaker support: can simulate speakers of different genders and styles to increase the diversity of speech.
- Efficient interface: provides a simple and easy-to-use Python API for quick integration into existing projects.
- Multi-language support: supports Chinese and English, is suitable for multi-language environments and meets the needs of users of different languages.
- Large-scale data training: about 100,000 hours of Chinese and English data are used for training, making the speech synthesis high-quality and natural-sounding.
- Open source: The project is open source, encourages further research and innovation, and provides pre-trained models.
- Ease of use: only text information is needed as input to generate the corresponding speech file, which is convenient for users with speech synthesis needs4.
- Conversation task compatibility: Suitable for handling conversation tasks that are usually assigned to large language models (LLMs), providing a more natural and smooth interactive experience.
- Control and security: Committed to improving the controllability of the model, adding watermarks, and integrating it with LLMs to ensure the security and reliability of the model.

### Related learning materials

- [Official documentation](https://chattts.com/zh)
- [Official warehouse](https://github.com/2noise/ChatTTS?ref=upstract.com)

Tutorial:
- [Article of How to install and use](https://blog.csdn.net/weixin_72543266/article/details/139389294?ops_request_misc=%257B%2522request%255Fid%2522%253A%2522172335258516800175793893%2522%252C%2522scm%2522%253A%252220140713.130102334..%2522%257D&request_id=172335258516800175793893&biz_id=0&utm_medium=distribute.pc_search_result.none-task-blog-2~all~top_positive~default-1-139389294-null-null.142^v100^pc_search_result_base5&utm_term=chat%20tts&spm=1018.2226.3001.4187)

[Reference project](https://github.com/adi-panda/Kuebiko)

### Alternative plan:

SoVits, short for Soft Voice Synthesis, is a deep learning-based voice synthesis technology designed to generate high-quality, personalized speech. Its main features and technical characteristics are as follows:

Key Features:
- High-Quality Voice Synthesis: SoVits can generate extremely natural and lifelike speech, with audio quality that closely resembles human voices.
- Personalized Voice Generation: SoVits can be trained to produce the voice of a specific person, making it ideal for scenarios where mimicking a particular voice or creating a unique voice style is required.
- Music and Song Synthesis: In addition to standard speech, SoVits excels at generating songs, handling complex pitch variations and tone expression, and is widely used in synthesizing voices for virtual singers.

Technical Characteristics:
- Based on the VITS Framework: SoVits is built on the VITS (Variational Inference Text-to-Speech) framework, which combines autoregressive and non-autoregressive models, enabling it to generate high-fidelity speech efficiently.
- Combination of Autoregressive and Non-Autoregressive Models: VITS leverages the high-quality generation capabilities of autoregressive models, combined with the efficiency of non-autoregressive models, resulting in fast synthesis with superior quality.
- End-to-End Training: SoVits supports end-to-end training, where the entire process from text to speech is handled by a single model, reducing the loss of information that might occur in intermediate steps.
- Conditional Generation: SoVit