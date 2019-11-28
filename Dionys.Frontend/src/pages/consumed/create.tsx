import React from "react";
import { Breadcrumbs } from "@material-ui/core";
import { History } from "history";
import { Layout, Link, ConsumedEdit } from "../../components";
import { ServiceProvider } from "../../services";
import { IConsumed } from "../../models";

interface ICreateProps {
    readonly history: History;
}

interface ICreateState {
    readonly data: IConsumedCreateData;
}

interface IConsumedCreateData {
    productId: string;
    weight: number;
}

class Create extends React.Component<ICreateProps, ICreateState> {
    protected readonly service = ServiceProvider.consumedService;

    public constructor(props: ICreateProps) {
        super(props);

        this.state = { data: { productId: "", weight: 0 } };

        this.onModelChange = this.onModelChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    public async componentDidMount() {
        const consumed: IConsumedCreateData = {
            productId: "",
            weight: 0
        };

        this.setState({ data: consumed });
    }

    protected onModelChange(consumed: IConsumed) {
        this.setState({ data: consumed });
    }

    protected async onSubmit() {
        const data = { ...this.state.data } as IConsumed;

        const consumed = await this.service.create(data);
        this.props.history.push(`/consumed/${consumed.id}`);
    }

    public render() {
        const consumed = this.state.data;

        return (
            <Layout title="Add consumed product">
                <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
                    <Link to="/">Home</Link>
                    <Link to="/consumed">Consumed list</Link>
                </Breadcrumbs>

                <ConsumedEdit consumed={consumed} onModelChange={this.onModelChange} onSubmit={this.onSubmit} />
            </Layout>
        );
    }
}

export default Create;
