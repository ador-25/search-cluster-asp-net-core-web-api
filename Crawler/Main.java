package com.company;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.HashSet;
import java.io.File;
import java.io.IOException;


public class Main {

    private static final int MAX_DEPTH = 2;
    private static HashSet<String> urlLinks;
    public Main() {
        urlLinks = new HashSet<>();
    }

    public void getPageLinks(String URL, int depth) {

        if ((!urlLinks.contains(URL) && (depth <MAX_DEPTH))) {
            System.out.println(">> Depth: " + depth + " [" + URL + "]");
            try {
                urlLinks.add(URL);
                // add scrap here
                Document doc = Jsoup.connect(URL).get();
                Elements availableLinksOnPage = doc.select("a[href]");
                depth++;
                for (Element page : availableLinksOnPage) {
                    getPageLinks(page.attr("abs:href"), depth);
                }
            }
            catch (IOException e) {
                System.err.println("For '" + URL + "': " + e.getMessage());
            }
            System.out.println(urlLinks);
        }
    }



    public void download(String urlLink, File fileLoc) {
        try {

            byte[] buffer = new byte[1024];
            double TotalDownload = 0.00;
            int readbyte = 0; //Stores the number of bytes written in each iteration.
            double percentOfDownload = 0.00;

            URL url = new URL(urlLink);
            HttpURLConnection http = (HttpURLConnection)url.openConnection();
            double filesize = (double)http.getContentLengthLong();

            BufferedInputStream input = new BufferedInputStream(http.getInputStream());
            FileOutputStream ouputfile = new FileOutputStream(fileLoc);
            BufferedOutputStream bufferOut = new BufferedOutputStream(ouputfile, 1024);
            while((readbyte = input.read(buffer, 0, 1024)) >= 0) {
                //Writing the content onto the file.
                bufferOut.write(buffer,0,readbyte);
                //TotalDownload is the total bytes written onto the file.
                TotalDownload += readbyte;
                //Calculating the percentage of download.
                percentOfDownload = (TotalDownload*100)/filesize;
                //Formatting the percentage up to 2 decimal points.
                String percent = String.format("%.2f", percentOfDownload);
                System.out.println("Downloaded "+ percent + "%");
            }
            System.out.println("Your download is now complete.");
            bufferOut.close();
            input.close();
        }
        catch(IOException e){
            e.printStackTrace();
        }

    }




    public static void main(String[] args) {
        //Main m = new Main ();
        //m.getPageLinks("http://www.northsouth.edu/", 0);
       // System.out.println(urlLinks);
        //E:\asas
        //String link = "http://free.epubebooks.net/ebooks/books/harry-potter-book-1.pdf";
        //File fileLoc = new File("E:\\asas\\pdf\\HarryPotter.pdf");

        //Main d = new Main();
       // d.download(link, fileLoc);
        PDFParser parser = null;
        PDDocument pdDoc = null;
        COSDocument cosDoc = null;
        PDFTextStripper pdfStripper;

        String parsedText;
        String fileName = "E:\\Files\\Small Files\\PDF\\JDBC.pdf";
        File file = new File(fileName);
        try {
            parser = new PDFParser(new FileInputStream(file));
            parser.parse();
            cosDoc = parser.getDocument();
            pdfStripper = new PDFTextStripper();
            pdDoc = new PDDocument(cosDoc);
            parsedText = pdfStripper.getText(pdDoc);
            System.out.println(parsedText.replaceAll("[^A-Za-z0-9. ]+", ""));
        } catch (Exception e) {
            e.printStackTrace();
            try {
                if (cosDoc != null)
                    cosDoc.close();
                if (pdDoc != null)
                    pdDoc.close();
            } catch (Exception e1) {
                e1.printStackTrace();
            }

        }
    }
}
