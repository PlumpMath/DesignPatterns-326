using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Patrix patrix = new Patrix();

            patrix.LoadScene("HalfPaper");

            patrix.LoadScene("Matrix");

            Console.ReadKey();
        }
    }
    class Patrix
    {

        private PatrixSceneFactory GetGameScene(string gameSceneName)
        {

            return (PatrixSceneFactory)Assembly.Load("AbstractFactoryExample").CreateInstance("AbstractFactoryExample." + gameSceneName);

        }



        public void LoadScene(string gameSceneName)
        {

            PatrixSceneFactory psf = GetGameScene(gameSceneName);

            Texture texture = psf.CreateTexture();

            Model model = psf.CreateModel();

            model.FillTexture(texture);

        }

    }

    //定义抽象工厂

    abstract class PatrixSceneFactory
    {
        public abstract Model CreateModel();

        public abstract Texture CreateTexture();
    }

    //定义抽象属性

    abstract class Model
    {
        public abstract void FillTexture(Texture texture);
    }

    //定义抽象属性

    abstract class Texture
    {
    }

    class HalfPaper : PatrixSceneFactory
    {
        public override Model CreateModel()
        {
            return new HalfPaperModel();
        }

        public override Texture CreateTexture()
        {
            return new HalfPaperTexture();
        }
    }

    class HalfPaperModel:Model
    {
        public HalfPaperModel()
        {
            Console.WriteLine("HalfPaper Model Created");
        }
        public override void FillTexture(Texture texture)
        {
            Console.WriteLine("HalfPaper Model is filled Texture");
        }
    }

    class HalfPaperTexture:Texture
    {
        public HalfPaperTexture()
        {
            Console.WriteLine("HalfPaper Texture Created");
        }
    }

    class Matrix : PatrixSceneFactory
    {

        public override Model CreateModel()
        {
            return new MatrixModel();
        }

        public override Texture CreateTexture()
        {
            return new MatrixTexture();
        }

        class MatrixModel : Model
        {
            public MatrixModel()
            {
                Console.WriteLine("Matrix Model Created");
            }
            public override void FillTexture(Texture texture)
            {
                Console.WriteLine("Matrix Model is filled Texture");
            }
        }

        class MatrixTexture : Texture
        {
            public MatrixTexture()
            {
                Console.WriteLine("Matrix Texture Created");
            }
        }
    }
}
