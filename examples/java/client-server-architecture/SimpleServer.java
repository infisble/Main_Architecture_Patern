import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class SimpleServer {
    public static void main(String[] args) throws Exception {
        int port = 5057;
        try (ServerSocket serverSocket = new ServerSocket(port)) {
            System.out.println("[Java Server] Listening on port " + port);

            try (Socket client = serverSocket.accept();
                 BufferedReader reader = new BufferedReader(
                     new InputStreamReader(client.getInputStream(), StandardCharsets.UTF_8));
                 BufferedWriter writer = new BufferedWriter(
                     new OutputStreamWriter(client.getOutputStream(), StandardCharsets.UTF_8))) {

                String message = reader.readLine();
                System.out.println("[Java Server] Received: " + message);

                String response = "ACK: " + message.toUpperCase();
                writer.write(response);
                writer.newLine();
                writer.flush();
            }
        }
    }
}
