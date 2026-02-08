import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class SimpleClient {
    public static void main(String[] args) throws Exception {
        String host = "127.0.0.1";
        int port = 5057;

        try (Socket socket = new Socket(host, port);
             BufferedWriter writer = new BufferedWriter(
                 new OutputStreamWriter(socket.getOutputStream(), StandardCharsets.UTF_8));
             BufferedReader reader = new BufferedReader(
                 new InputStreamReader(socket.getInputStream(), StandardCharsets.UTF_8))) {

            writer.write("hello from java client");
            writer.newLine();
            writer.flush();

            String response = reader.readLine();
            System.out.println("[Java Client] " + response);
        }
    }
}
