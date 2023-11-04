# TyDrone
Navigate your Tello drone with a Mixed Reality virtual cockpit interface. A new dimension of drone control on your MR device.

![TyDrone](./tydrone-01.gif)

## Requirements
- Drone:
  - [Tello](https://www.ryzerobotics.com/tello)
- Mixed Reality Devices:
  - [Meta Quest 2](https://www.oculus.com/quest-2/)
  - [Meta Quest 3](https://www.oculus.com/quest-3/)
  - [Meta Quest Pro](https://www.oculus.com/quest-pro/)

## Usage
Please refer to the [User's Manual](https://github.com/ototadana/TyDrone/wiki/TyDrone-User's-Manual) for instructions on how to use this software.

## How to Build
1. Clone this repository.
2. Restore [MRTK2 Features](https://github.com/microsoft/MixedRealityToolkit-Unity) using the [Mixed Reality Feature Tool](https://www.microsoft.com/en-us/download/details.aspx?id=102778).
3. Copy the [tydrone-android-plugin (.aar files)](https://github.com/ototadana/tydrone-android) to `Assets/Plugins/Android` folder.
4. Launch Unity.
5. Import [Oculus Integration v57.0](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022) using the Package Manager.
6. Open the TyDrone Scene located at `Assets/Scenes/TyDrone`.
7. Switch the platform to Android and build.

## License
This software is released under the MIT License, see [LICENSE](./LICENSE).
See [ACKNOWLEDGEMENTS](./ACKNOWLEDGEMENTS) for works on which this software depends.
