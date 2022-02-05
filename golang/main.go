package main

import (
	"fmt"
	"io/fs"
	"os"
	"strings"

	"github.com/yuin/charsetutil"
)

func main() {
	var inputFile string
	if len(os.Args) < 2 {
		fmt.Println("Choose File to convert:")

		fmt.Scanln(&inputFile)
	} else {
		inputFile = os.Args[1]
	}
	var file, err = os.ReadFile(inputFile)
	errCheck(err)
	fmt.Println("Which Encoding has the Inputfile?  (1) UTF8 (2) Latin1/ISO-8859-1 (3) Windows-1252 Western Europe")
	var readLine string
	fmt.Scanln(&readLine)
	var encoding string
	switch readLine {
	case "1":
		encoding = "utf-8"
		break
	case "2":
		encoding = "iso-8859-1"
		break
	}
	fmt.Println("To which Encoding should it be converted? (1) UTF8 (2) Latin1/ISO-8859-1/Windows-1252 Western Europe")
	fmt.Scanln(&readLine)
	var outputEncoding string
	switch readLine {
	case "1":
		outputEncoding = "utf-8"
		break
	case "2":
		outputEncoding = "iso-8859-1"
		break
	}
	var outputFile []byte
	if encoding == "UTF-8" {
		outputFile, err = charsetutil.EncodeBytes(file, outputEncoding)
		errCheck(err)
	} else {
		var buffer, err = charsetutil.DecodeBytes(file, encoding)
		errCheck(err)
		outputFile, err = charsetutil.EncodeString(buffer, outputEncoding)
		errCheck(err)
	}
	var newname = substr(inputFile, 0, strings.LastIndex(inputFile, "."))
	newname += "_" + outputEncoding + substr(inputFile, strings.LastIndex(inputFile, "."), 1000000)
	var filemode fs.FileInfo
	filemode, err = os.Stat(inputFile)
	errCheck(err)
	os.WriteFile(newname, outputFile, filemode.Mode())
}
func errCheck(err error) {
	if err != nil {
		panic(err)
	}

}
func substr(s string, start, end int) string {
	counter, startIdx := 0, 0
	for i := range s {
		if counter == start {
			startIdx = i
		}
		if counter == end {
			return s[startIdx:i]
		}
		counter++
	}
	return s[startIdx:]
}
