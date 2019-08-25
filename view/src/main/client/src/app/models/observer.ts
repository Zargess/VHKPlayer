export class Observer<T> {
  constructor(private func: (T) => void) {}

  public notify(value: T) {
    this.func(value);
  }
}
