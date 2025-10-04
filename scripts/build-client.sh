#!/usr/bin/env bash
set -euo pipefail
# Build Unity project in batchmode (works on CI)
PROJECT_DIR="\$(cd "\$(dirname "\${BASH_SOURCE[0]}")/.." && pwd)/client-unity"
UNITY_PATH="\${UNITY_PATH:-/opt/Unity/Hub/Editor/${UNITY_VERSION}/Editor/Unity}" # adjust if needed
BUILD_OUTPUT="\$PROJECT_DIR/Builds/linux"

echo "Building Unity project..."
if [ ! -x "\$UNITY_PATH" ]; then
  echo "Unity executable not found at \$UNITY_PATH. Set UNITY_PATH env var to point to Unity Editor."
  exit 1
fi

"\$UNITY_PATH" -quit -batchmode -projectPath "\$PROJECT_DIR" -buildLinux64Player "\$BUILD_OUTPUT/playfab_client.x86_64" -nographics -silent-crashes -logFile "\$BUILD_OUTPUT/build.log"
echo "Build finished. Output: \$BUILD_OUTPUT"
