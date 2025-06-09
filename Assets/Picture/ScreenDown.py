from PIL import Image
import os

# === 設定 ===
folder_path = r"C:\Users\tomy\Documents\Unity\Shot_Ball\Assets\Picture"

output_path = os.path.join(folder_path, "resized")  # 保存先フォルダ
fixed_height = 500  # すべての画像の縦サイズ

print("実行!!")
# フォルダが存在しなければ作成
os.makedirs(output_path, exist_ok=True)

# 拡張子の対象
extensions = [".jpg", ".jpeg", ".png"]

# === リサイズ処理 ===
for filename in os.listdir(folder_path):
    if any(filename.lower().endswith(ext) for ext in extensions):
        input_path = os.path.join(folder_path, filename)
        try:
            img = Image.open(input_path)
            width, height = img.size

            # 新しい横幅をアスペクト比から計算
            new_width = int((fixed_height / height) * width)
            resized_img = img.resize((new_width, fixed_height), Image.LANCZOS)

            # 保存
            output_file = os.path.join(output_path, filename)
            resized_img.save(output_file)

            print(f"{filename} を {new_width}x{fixed_height} にリサイズしました")
        except Exception as e:
            print(f"{filename} の処理中にエラー: {e}")
