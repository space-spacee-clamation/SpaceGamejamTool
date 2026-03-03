import os

def merge_cs_files(source_dir, output_file):
    """
    将 source_dir 及其子目录下所有 .cs 文件合并到 output_file 中
    """
    # 确保输出文件所在的目录存在
    output_dir = os.path.dirname(output_file)
    if output_dir and not os.path.exists(output_dir):
        os.makedirs(output_dir)

    try:
        with open(output_file, 'w', encoding='utf-8') as outfile:
            file_count = 0
            # 遍历文件夹（包括子文件夹）
            for root, dirs, files in os.walk(source_dir):
                for file in files:
                    if file.endswith('.cs'):
                        file_path = os.path.join(root, file)

                        # 写入文件名作为分隔符，方便查看
                        outfile.write(f"\n{'='*50}\n")
                        outfile.write(f"FILE: {file_path}\n")
                        outfile.write(f"{'='*50}\n\n")

                        # 读取 .cs 文件内容并写入 .txt
                        # 使用 errors='ignore' 防止因为个别文件编码问题导致程序崩溃
                        with open(file_path, 'r', encoding='utf-8', errors='ignore') as infile:
                            outfile.write(infile.read())
                            outfile.write("\n") # 每个文件后加一个换行

                        file_count += 1
                        print(f"已处理: {file}")

            print(f"\n处理完成！共合并了 {file_count} 个 .cs 文件。")
            print(f"输出文件位于: {os.path.abspath(output_file)}")

    except Exception as e:
        print(f"发生错误: {e}")

if __name__ == "__main__":
    # --- 配置区域 ---
    # 填写你的 .cs 文件所在文件夹路径
    input_folder = r'D:\SpaceGamejamTool\Assets\Script'
    # 填写你想要保存的 .txt 文件路径
    result_txt = 'merged_code.txt'
    # ----------------

    merge_cs_files(input_folder, result_txt)
