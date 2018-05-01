using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graphics {
    abstract class Window {

        protected uint windowWidth = 800;
        protected uint windowHeight = 600;
        protected RenderWindow renderWindow;
        protected Color windowColor = new Color(0, 0, 0);


        static void OnClose(object sender, EventArgs e) {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        public void StartWithNewThread() {
            Thread d = new Thread(new ThreadStart(Start));
            d.Start();
        }

        public void Start() {
            renderWindow = new RenderWindow(new VideoMode(windowWidth, windowHeight), GetWindowName());
            renderWindow.Closed += new EventHandler(OnClose);

            while (renderWindow.IsOpen) {
                renderWindow.DispatchEvents();
                renderWindow.Clear(windowColor);
                Update();
                renderWindow.Display();
            }
        }

        public void Close() {
            renderWindow.Close();
        }

        protected abstract void Update();
        protected abstract String GetWindowName();
    }
}
