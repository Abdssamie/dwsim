import os

target_dir = "."
old_tfm = "net8.0"
new_tfm = "net10.0"

for root, dirs, files in os.walk(target_dir):
    for file in files:
        if file.endswith((".csproj", ".vbproj")):
            file_path = os.path.join(root, file)
            with open(file_path, "r", encoding="utf-8") as f:
                content = f.read()
            
            if old_tfm in content:
                print(f"Updating {file_path}")
                updated_content = content.replace(f"<{old_tfm}>", f"<{new_tfm}>") # Handle <TargetFrameworks>
                updated_content = updated_content.replace(f">{old_tfm}<", f">{new_tfm}<") # Handle <TargetFramework>
                
                with open(file_path, "w", encoding="utf-8") as f:
                    f.write(updated_content)
