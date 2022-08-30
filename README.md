# NeosPlus
NeosPlus is a NeosVR plugin that adds extra Logix nodes and features. The goal behind it is to add features that were requested by the users of Neos.  
  
# Installation
Important: This is a plugin, not a mod! Installation is different between the two.  
To install this, download `NEOSPlus.dll` from the Releases tab and place it in the `Libraries` folder in your Neos directory.  
Next, make Neos load it by using the `-LoadAssembly` launch argument.  
When you next start Neos, Neos Plus should also be loaded and work.
  

## Features that have been added so far
## Nodes
# Avatar
- Logix/Avatar/NearestUser
# Json
- Logix/Json/JsonAddToObject
- Logix/Json/JsonAppendToArray
- Logix/Json/JsonCountArrayChildren
- Logix/Json/JsonCountObjectChildren
- Logix/Json/JsonEmpty
- Logix/Json/JsonGetFromArray
- Logix/Json/JsonGetFromObject
- Logix/Json/JsonInfo
- Logix/Json/JsonInsertToArray
- Logix/Json/JsonParseString
- Logix/Json/JsonParseStringArray
- Logix/Json/JsonQuickGetFromObject
- Logix/Json/JsonRemoveFromArray
- Logix/Json/JsonRemoveFromObject
- Logix/Json/JsonToString
# Locomotion
- Logix/Locomotion/IsUserInNoclip
# Math Matrix
- Logix/Math/Matrix/GaussianElimination_double2x2
- Logix/Math/Matrix/GaussianElimination_double3x3
- Logix/Math/Matrix/GaussianElimination_double4x4
- Logix/Math/Matrix/GaussianElimination_float2x2
- Logix/Math/Matrix/GaussianElimination_float3x3
- Logix/Math/Matrix/GaussianElimination_float4x4
- Logix/Math/Matrix/GaussJordanElimination_double2x2
- Logix/Math/Matrix/GaussJordanElimination_double3x3
- Logix/Math/Matrix/GaussJordanElimination_double4x4
- Logix/Math/Matrix/GaussJordanElimination_float2x2
- Logix/Math/Matrix/GaussJordanElimination_float3x3
- Logix/Math/Matrix/GaussJordanElimination_float4x4
- Logix/Math/Matrix/MatrixClass
# Math Random
- Logix/Math/Random/RandomBool2
- Logix/Math/Random/RandomBool3
- Logix/Math/Random/RandomBool4
- Logix/Math/Random/RandomCharacter
- Logix/Math/Random/RandomDouble
- Logix/Math/Random/RandomInt2
- Logix/Math/Random/RandomInt3
- Logix/Math/Random/RandomInt4
- Logix/Math/Random/RandomLetter
# Math
- Logix/Math/EulersToientFunction
- Logix/Math/Factorial
- Logix/Math/SortNode
- Logix/Math/MinMax
# Operators
- Logix/Operators/ZeroOneI
- Logix/Operators/DoubleInput
# Playback
- Logix/Playback/IsPaused
- Logix/Playback/IsStopped
# Slots
- Logix/Slots/CreateEmptySlot
- Logix/Slots/GetGrandparent
# String
- Logix/String/CountSubstring
- Logix/String/DecodeBase64
- Logix/String/EncodeBase64
- Logix/String/EndcodeMD5
- Logix/String/EncodeSha256
- Logix/String/HammingDistance
- Logix/String/HammingDistanceNonNullable
# Users
- Logix/Users/IsUserInSeatedMode

## Components
# Wizards
- Add-ons/Wizards/MeshColliderManagementTools

## Materials
- Assets/Materials/NeosPlus/TestMaterial