/* eslint-disable jsx-a11y/label-has-associated-control */
import PageLayout from './components/PageLayout';

function App() {
  const Form = () => {
    return (
      <div className="form-control w-3/5">
        <label className="label">
          <span className="label-text">Enter url</span>
        </label>
        <label className="input-group input-group-md">
          <input
            type="text"
            placeholder="https://github.com/nathanhettige"
            className="input input-md input-bordered w-full"
          />
          <button className="btn btn-md">Shorten!</button>
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
