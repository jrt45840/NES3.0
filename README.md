What is NES 3.0?
NES 3.0 is an extension of the traditional NES ROM format that allows you to embed additional metadata and artwork directly within the ROM file. This is achieved by appending a special footer to the original NES file, which includes:

Metadata: Structured data about the game, including:

Title (string)

Region (integer, e.g., USA, Europe)

Revision (integer, e.g., Rev 1 to Rev 10)

Beta (integer, e.g., Beta 1 to Beta 10)

Proto (integer, e.g., Proto 1 to Proto 10)

Demo (integer, e.g., Demo 1 to Demo 10)

Licensed (boolean, Licensed or Unlicensed)

Aftermarket (boolean, Yes or No)

Pirate (boolean, Yes or No)

Translation (string, e.g., "T-En By*")

Description (string)

Images: Base64-encoded images for:

Front Cover

Back Cover

Cartridge

Why NES 3.0?
This new format allows for a richer set of information to be embedded directly within the ROM file without altering the game data itself. This means you can maintain compatibility with existing emulators while also providing additional context and visual appeal. It’s perfect for enthusiasts who want to archive games with detailed information and artwork.

How It Works
Embedding Metadata and Images: Using a custom footer, metadata and images are appended to the end of the NES ROM file. The metadata is serialized and images are converted to Base64 strings.

Reading Metadata: When loading a NES 3.0 file, the footer is read to extract and deserialize metadata and images.

Compatibility: The NES 3.0 format retains the original NES data intact, ensuring that games can still be played on standard NES emulators.
