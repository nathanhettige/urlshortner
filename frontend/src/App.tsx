/* eslint-disable jsx-a11y/label-has-associated-control */
import PageLayout from './components/PageLayout';

function App() {
  const Form = () => {
    return (
      <div className="form-control">
        <label className="label">
          <span className="label-text">Enter url</span>
        </label>
        <label className="input-group">
          <input
            type="text"
            placeholder="http://google.com"
            className="input input-bordered"
          />
          <button className="btn">Shorten!</button>
        </label>
      </div>
    );
  };

  return (
    <>
      <PageLayout>
        <section className="flex flex-col items-center gap-5 h-full">
          <h1 className="text-4xl">Create Short Links!</h1>
          <Form />
        </section>
      </PageLayout>
    </>
  );
}

export default App;
