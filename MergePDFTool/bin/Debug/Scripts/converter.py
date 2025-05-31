import sys
import os

def convert_pdf_to_docx(input_path, output_path):
    from pdf2docx import Converter
    cv = Converter(input_path)
    cv.convert(output_path, start=0, end=None)
    cv.close()

def convert_docx_to_pdf(input_path, output_path):
    try:
        from docx2pdf import convert
        convert(input_path, output_path)
    except Exception as e:
        print("Error during DOCX to PDF:", e)

if __name__ == "__main__":
    if len(sys.argv) < 4:
        print("Usage: python script.py <mode> <input> <output>")
        sys.exit(1)

    mode = sys.argv[1].lower()
    input_file = sys.argv[2]
    output_file = sys.argv[3]

    if not os.path.exists(input_file):
        print("Input file not found:", input_file)
        sys.exit(1)

    if mode == "pdf2docx":
        convert_pdf_to_docx(input_file, output_file)
    elif mode == "docx2pdf":
        convert_docx_to_pdf(input_file, output_file)
    else:
        print("Invalid mode. Use 'pdf2docx' or 'docx2pdf'")