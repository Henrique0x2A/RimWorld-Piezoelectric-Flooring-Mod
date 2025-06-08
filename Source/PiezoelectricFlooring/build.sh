#!/bin/bash
echo "Building PiezoelectricFlooring mod..."

# Navigate to source directory
cd Source/PiezoelectricFlooring

# Clean previous builds
rm -f ../../Assemblies/PiezoelectricFlooring.dll

# Build the project
dotnet build --configuration Release

# Check if build was successful
if [ -f "../../Assemblies/PiezoelectricFlooring.dll" ]; then
    echo "✅ Build successful! Assembly created at Assemblies/PiezoelectricFlooring.dll"
    echo "📦 Mod is ready for testing"
else
    echo "❌ Build failed - check the output above for errors"
    exit 1
fi